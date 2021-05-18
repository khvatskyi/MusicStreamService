using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MusicStreamServiceApp.BLL.Interfaces.IServices;
using MusicStreamServiceApp.DAL.Interfaces;
using MusicStreamServiceApp.DAL.Entities;
using MusicStreamServiceApp.BLL.DTOs;
using AutoMapper;

namespace MusicStreamServiceApp.BLL.Services
{
    public class AlbumService : BaseService, IAlbumService
    {
        public AlbumService(IUnitOfWork unitOfWork, IMapper mapper)
           : base(unitOfWork, mapper)
        {
        }

        public async Task AddAlbumAsync(AlbumDTO albumDTO)
        {
            var album = Mapper.Map<Album>(albumDTO);

            await UnitOfWork.AlbumRepository.Add(album);
        }

        public async Task DeleteAlbumAsync(int Id)
        {
            var album = await UnitOfWork.AlbumRepository.Get(Id);

            await UnitOfWork.AlbumRepository.Delete(album);
        }

        public async Task<AlbumDTO> GetAlbumAsync(int Id)
        {
            var album = await UnitOfWork.AlbumRepository.Get(Id);

            return Mapper.Map<AlbumDTO>(album);
        }

        public async Task<AlbumDTO> GetAlbumAsync(string Name, string Author)
        {
            var album = await UnitOfWork.AlbumRepository.Get(Name, Author);

            return Mapper.Map<AlbumDTO>(album);
        }

        public async Task<IEnumerable<AlbumDTO>> GetAllAlbumsAsync()
        {
            var albums = await UnitOfWork.AlbumRepository.GetAll();

            return Mapper.Map<IEnumerable<AlbumDTO>>(albums);
        }

        public async Task<IEnumerable<AlbumDTO>> GetAuthorAlbumsAsync(string Author)
        {
            if (Author == null)
            {
                throw new Exception("Author's name is null");
            }

            var albums = await UnitOfWork.AlbumRepository.GetAuthorAlbums(Author);

            if(albums == null)
            {
                throw new Exception("Not found");
            }

            return Mapper.Map<IEnumerable<AlbumDTO>>(albums);
        }

        public async Task<bool> IsAnyAlbumDefinedAsync(int Id)
        {
            return await UnitOfWork.AlbumRepository.Any(Id);
        }

        public async Task UpdateAlbumAsync(AlbumDTO albumDTO)
        {
            var album = Mapper.Map<Album>(albumDTO);

            await UnitOfWork.AlbumRepository.Update(album);
        }
    }
}
