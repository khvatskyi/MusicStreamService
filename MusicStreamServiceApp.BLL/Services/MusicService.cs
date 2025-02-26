using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MusicStreamServiceApp.BLL.Interfaces.IServices;
using MusicStreamServiceApp.DAL.Interfaces;
using MusicStreamServiceApp.DAL.Entities;
using System.Linq;
using AutoMapper;
using MusicStreamServiceApp.BLL.DTOs;

namespace MusicStreamServiceApp.BLL.Services
{
    public class MusicService : BaseService, IMusicService
    {
        public MusicService(IUnitOfWork unitOfWork,
            IMapper mapper)
            : base(unitOfWork, mapper)
        {
        }

        public async Task<bool> IsAnyMusicDefinedAsync(int Id)
        {
            return await UnitOfWork.MusicRepository.Any(Id);
        }

        public async Task AddMusicAsync(MusicCUDTO musicCreateDTO)
        {
            await CheckGenreAsync(musicCreateDTO.Genre);
            var music = Mapper.Map<Music>(musicCreateDTO);
            music.AlbumId = await CheckAlbumAsync(musicCreateDTO.Album, musicCreateDTO.Author);
            await UnitOfWork.MusicRepository.Add(music);
            music = await UnitOfWork.MusicRepository.Get(music.Name, music.Author, music.Year);
            await AddMusicGenreRecordAsync(music.Id, musicCreateDTO.Genre);
        }

        public async Task DeleteMusicAsync(MusicViewDTO musicDTO)
        {
            var music = await UnitOfWork.MusicRepository.Get(musicDTO.Name, musicDTO.Author, musicDTO.Year);
            await UnitOfWork.MusicRepository.Delete(music);
        }

        public async Task<IEnumerable<MusicViewDTO>> GetAllMusicAsync()
        {
            IEnumerable<Music> musics = await UnitOfWork.MusicRepository.GetAll();
            return Mapper.Map<IEnumerable<MusicViewDTO>>(musics);
        }

        public async Task<IEnumerable<MusicViewDTO>> GetMusicByAlbumAsync(int AlbumId)
        {
            var musicList = await UnitOfWork.MusicRepository.GetByAlbumId(AlbumId);
            return Mapper.Map<IEnumerable<MusicViewDTO>>(musicList);
        }

        public async Task<IEnumerable<MusicViewDTO>> GetMusicByNameAsync(string Name)
        {
            var musicList = await UnitOfWork.MusicRepository.GetByName(Name);
            return Mapper.Map<List<MusicViewDTO>>(musicList);
        }

        public async Task<MusicViewDTO> GetMusicForViewAsync(int Id)
        {
            var music = await UnitOfWork.MusicRepository.Get(Id);
            return Mapper.Map<MusicViewDTO>(music);
        }

        public async Task<MusicCUDTO> GetMusicForUpdateAsync(int Id)
        {
            var music = await UnitOfWork.MusicRepository.Get(Id);
            return await ToFillMusicCUDTORecordsAsync(music);
        }

        public async Task<MusicCUDTO> GetMusicForUpdateAsync(string Name, string Author, int? Year)
        {
            var music = await UnitOfWork.MusicRepository.Get(Name, Author, Year);
            return await ToFillMusicCUDTORecordsAsync(music);
        }

        public async Task UpdateMusicAsync(int Id, MusicCUDTO musicDTO)
        {
            await CheckGenreAsync(musicDTO.Genre);
            var music = Mapper.Map<Music>(musicDTO);
            music.Id = Id;
            music.AlbumId = await CheckAlbumAsync(musicDTO.Album, musicDTO.Author);
            await UnitOfWork.MusicRepository.Update(music);
            await AddMusicGenreRecordAsync(music.Id, musicDTO.Genre);
        }

        private async Task<MusicCUDTO> ToFillMusicCUDTORecordsAsync(Music music)
        {
            var musicDTO = Mapper.Map<MusicCUDTO>(music);
            if (musicDTO == null)
            {
                return musicDTO;
            }

            if (music.AlbumId != null)
            {
                var album = await UnitOfWork.AlbumRepository.Get(music.AlbumId.Value);

                musicDTO.Album = album.Name;
            }
            else
            {
                musicDTO.Album = "";
            }
            music.MusicGenres = await UnitOfWork.MusicGenreRepository.GetByMusicId(music.Id);
            if (music.MusicGenres != null)
            {
                foreach (var item in music.MusicGenres)
                {
                    item.Genre = await UnitOfWork.GenreRepository.Get(item.GenreId);
                }

                int i = 0;
                foreach (var item in music.MusicGenres)
                {
                    if (i < music.MusicGenres.Count() - 1)
                    {
                        musicDTO.Genre += item.Genre.Name + ", ";
                    }
                    else
                    {
                        musicDTO.Genre += item.Genre.Name;
                    }
                    i++;
                }
            }
            if (musicDTO.Genre == null)
            {
                musicDTO.Genre = "";
            }
            return musicDTO;
        }
        private async Task<int?> CheckAlbumAsync(string AlbumName, string AuthorName)
        {
            if (!string.IsNullOrWhiteSpace(AlbumName))
            {
                var album = await UnitOfWork.AlbumRepository.Get(AlbumName, AuthorName);
                if (album == null)
                {
                    throw new Exception("Album not found");
                }
                return album.Id;
            }
            return null;
        }
        private async Task CheckGenreAsync(string GenreName)
        {
            var genre = await UnitOfWork.GenreRepository.GetByName(GenreName);
            if (genre == null)
            {
                throw new Exception("Incorrect genre name");
            }
        }
        private async Task AddMusicGenreRecordAsync(int musicId, string genreName)
        {
            var musicgenreList = await UnitOfWork.MusicGenreRepository.GetByMusicId(musicId);
            foreach(var musicGenre in musicgenreList)
            {
                await UnitOfWork.MusicGenreRepository.Delete(musicGenre);
            }
            var genre = await UnitOfWork.GenreRepository.GetByName(genreName);
            var mg = new MusicGenre
            {
                MusicId = musicId,
                GenreId = genre.Id,
            };
            await UnitOfWork.MusicGenreRepository.Add(mg);
        }
    }
}
