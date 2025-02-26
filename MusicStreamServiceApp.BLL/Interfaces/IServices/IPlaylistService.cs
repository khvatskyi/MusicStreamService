using MusicStreamServiceApp.BLL.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MusicStreamServiceApp.BLL.Interfaces.IServices
{
    public interface IPlaylistService
    {
        Task AddPlaylistAsync(string UserId, PlaylistCUDTO playlistDTO);
        Task AddMusicToPlaylistAsync(MusicPlaylistDTO musicPlaylistDTO);
        Task DeletePlaylistAsync(PlaylistDTO playlistDTO);
        Task DeleteMusicFromPlaylistAsync(MusicPlaylistDTO musicPlaylistDTO);
        Task<IEnumerable<PlaylistDTO>> GetAllAsync();
        Task<IEnumerable<PlaylistDTO>> GetPlaylistDTOListAsync(string UserId);
        Task<PlaylistDTO> GetPlaylistAsync(string UserId, string PlaylistName);
        Task<PlaylistDTO> GetPlaylistAsync(int Id);
        Task UpdatePlaylistNameAsync(PlaylistDTO playlistDTO, string NewName);
    }
}
