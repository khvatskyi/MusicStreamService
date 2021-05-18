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
    public class GenreService : BaseService, IGenreService
    {
        public GenreService(IUnitOfWork unitOfWork, IMapper mapper)
            : base(unitOfWork, mapper)
        {
        }

        public async Task AddGenreAsync(GenreDTO genreDTO)
        {
            var genre = Mapper.Map<Genre>(genreDTO);

            await UnitOfWork.GenreRepository.Add(genre);
        }

        public async Task DeleteGenreAsync(int Id)
        {
            var genre = await UnitOfWork.GenreRepository.Get(Id);

            await UnitOfWork.GenreRepository.Delete(genre);
        }

        public async Task<GenreDTO> GetGenreAsync(int Id)
        {
            var genre = await UnitOfWork.GenreRepository.Get(Id);

            return Mapper.Map<GenreDTO>(genre);
        }

        public async Task<GenreDTO> GetGenreAsync(string Name)
        {
            var genre = await UnitOfWork.GenreRepository.GetByName(Name);

            return Mapper.Map<GenreDTO>(genre);
        }

        public async Task<IEnumerable<GenreDTO>> GetAllGenresAsync()
        {
            var genreList = await UnitOfWork.GenreRepository.GetAll();

            return Mapper.Map<IEnumerable<GenreDTO>>(genreList);
        }

        public async Task UpdateGenreAsync(GenreDTO genreDTO)
        {
            var genre = Mapper.Map<Genre>(genreDTO);

            await UnitOfWork.GenreRepository.Update(genre);
        }

        public async Task<bool> IsAnyGenreDefinedAsync(int Id)
        {
            return await UnitOfWork.GenreRepository.Any(Id);
        }
    }
}
