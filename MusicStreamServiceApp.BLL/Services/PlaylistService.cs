using AutoMapper;
using MusicStreamServiceApp.BLL.Services;
using MusicStreamServiceApp.DAL.Entities;
using MusicStreamServiceApp.DAL.Interfaces;
using MusicStreamServiceApp.BLL.DTOs;
using MusicStreamServiceApp.BLL.Interfaces.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicStreamServiceApp.BLL.Services
{
    public class PlaylistService : BaseService, IPlaylistService
    {
        public PlaylistService(IUnitOfWork unitOfWork,
             IMapper mapper)
             : base(unitOfWork, mapper)
        {
        }

        public async Task AddMusicToPlaylistAsync(MusicPlaylistDTO musicPlaylistDTO)
        {
            var musicPlaylist = Mapper.Map<MusicPlaylist>(musicPlaylistDTO);
            await UnitOfWork.MusicPlaylistRepository.Add(musicPlaylist);
        }
        public async Task AddPlaylistAsync(string UserId, PlaylistCUDTO playlistDTO)
        {
            var userPlaylist = Mapper.Map<UserPlaylist>(playlistDTO);
            userPlaylist.UserId = UserId;
            await UnitOfWork.UserPlaylistRepository.Add(userPlaylist);
        }
        public async Task DeleteMusicFromPlaylistAsync(MusicPlaylistDTO musicPlaylistDTO)
        {
            var musicPlaylist = await UnitOfWork
                .MusicPlaylistRepository
                    .GetByUserPlaylistIdAndMusicId(
                        musicPlaylistDTO.UserPlaylistId,
                        musicPlaylistDTO.MusicId);

            await UnitOfWork.MusicPlaylistRepository.Delete(musicPlaylist);
        }
        public async Task DeletePlaylistAsync(PlaylistDTO playlistDTO)
        {
            var userPlaylist = Mapper.Map<UserPlaylist>(playlistDTO);
            await UnitOfWork.UserPlaylistRepository.Delete(userPlaylist);
        }
        public async Task<IEnumerable<PlaylistDTO>> GetAllAsync()
        {
            var userPlaylists = await UnitOfWork.UserPlaylistRepository.GetAll();
            var PlaylistsDTO = Mapper.Map<IEnumerable<PlaylistDTO>>(userPlaylists);
            foreach (var playlist in PlaylistsDTO)
            {
                var musicPlaylistList = (await UnitOfWork.MusicPlaylistRepository.GetAll(playlist.Id)).ToList();

                var musicList = new List<Music>();

                foreach (var musicPlaylist in musicPlaylistList)
                {
                    musicList.Add(await UnitOfWork.MusicRepository.Get(musicPlaylist.MusicId));
                }

                playlist.MusicList = Mapper.Map<IEnumerable<MusicViewDTO>>(musicList);
            }
            return PlaylistsDTO;
        }
        public async Task<PlaylistDTO> GetPlaylistAsync(int Id)
        {
            var userPlaylist = await UnitOfWork.UserPlaylistRepository.Get(Id);

            var playlistDTO = Mapper.Map<PlaylistDTO>(userPlaylist);

            if (playlistDTO == null)
            {
                return playlistDTO;
            }

            var musicPlaylistList = (await UnitOfWork.MusicPlaylistRepository.GetAll(playlistDTO.Id)).ToList();

            var musicList = new List<Music>();

            foreach (var musicPlaylist in musicPlaylistList)
            {
                musicList.Add(await UnitOfWork.MusicRepository.Get(musicPlaylist.MusicId));
            }

            playlistDTO.MusicList = Mapper.Map<IEnumerable<MusicViewDTO>>(musicList);

            return playlistDTO;
        }
        public async Task<PlaylistDTO> GetPlaylistAsync(string UserId, string PlaylistName)
        {
            var userPlaylist = await UnitOfWork.UserPlaylistRepository.Get(UserId, PlaylistName);

            var playlistDTO = Mapper.Map<PlaylistDTO>(userPlaylist);

            if (playlistDTO == null)
            {
                return playlistDTO;
            }

            var musicPlaylistList = (await UnitOfWork.MusicPlaylistRepository.GetAll(playlistDTO.Id)).ToList();

            var musicList = new List<Music>();

            foreach (var musicPlaylist in musicPlaylistList)
            {
                musicList.Add(await UnitOfWork.MusicRepository.Get(musicPlaylist.MusicId));
            }

            playlistDTO.MusicList = Mapper.Map<IEnumerable<MusicViewDTO>>(musicList);

            return playlistDTO;
        }
        public async Task<IEnumerable<PlaylistDTO>> GetPlaylistDTOListAsync(string UserId)
        {
            var userPlaylist = await UnitOfWork.UserPlaylistRepository.GetByUserId(UserId);

            var userPlaylistDTO = Mapper.Map<IEnumerable<PlaylistDTO>>(userPlaylist);

            foreach (var playlist in userPlaylistDTO)
            {
                var musicPlaylistList = (await UnitOfWork.MusicPlaylistRepository.GetAll(playlist.Id)).ToList();

                var musicList = new List<Music>();

                foreach (var musicPlaylist in musicPlaylistList)
                {
                    musicList.Add(await UnitOfWork.MusicRepository.Get(musicPlaylist.MusicId));
                }

                playlist.MusicList = Mapper.Map<IEnumerable<MusicViewDTO>>(musicList);
            }
            return userPlaylistDTO;
        }
        public async Task UpdatePlaylistNameAsync(PlaylistDTO playlistDTO, string NewName)
        {
            var userPlaylist = Mapper.Map<UserPlaylist>(playlistDTO);
            userPlaylist.PlaylistName = NewName;
            await UnitOfWork.UserPlaylistRepository.Update(userPlaylist);
        }
    }
}