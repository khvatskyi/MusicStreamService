using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MusicStreamServiceApp.BLL.DTOs;
using MusicStreamServiceApp.DAL.Entities;

namespace MusicStreamServiceApp.BLL.Interfaces.IServices
{
    public interface IMusicService
    {
        Task<bool> IsAnyMusicDefinedAsync(int Id);

        Task<IEnumerable<MusicViewDTO>> GetAllMusicAsync();

        Task<IEnumerable<MusicViewDTO>> GetMusicByAlbumAsync(int AlbumId);

        Task<MusicViewDTO> GetMusicForViewAsync(int Id);

        Task<MusicCUDTO> GetMusicForUpdateAsync(int Id);

        Task<MusicCUDTO> GetMusicForUpdateAsync(string Name, string Author, int? Year);

        Task<IEnumerable<MusicViewDTO>> GetMusicByNameAsync(string Name);

        Task AddMusicAsync(MusicCUDTO musicCreateDTO);

        Task UpdateMusicAsync(int Id, MusicCUDTO musicUpdateDTO);
        
        Task DeleteMusicAsync(MusicViewDTO musicDTO);
    }
}
