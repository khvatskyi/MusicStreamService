using MusicStreamServiceApp.DAL.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MusicStreamServiceApp.DAL.Interfaces.IEntityRepositories
{
    public interface IMusicPlaylistRepository : IGenericRepository<MusicPlaylist, int>
    {
        Task<IEnumerable<MusicPlaylist>> GetAll(int UserPlaylistId);
        Task<MusicPlaylist> GetByUserPlaylistIdAndMusicId(int UserPlaylistId, int MusicId);
    }
}
