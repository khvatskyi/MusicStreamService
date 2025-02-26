using MusicStreamServiceApp.DAL.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MusicStreamServiceApp.DAL.Interfaces.IEntityRepositories
{
    public interface IUserPlaylistRepository : IGenericRepository<UserPlaylist, int>
    {
        Task<IEnumerable<UserPlaylist>> GetByUserId(string UserId);

        Task<UserPlaylist> Get(string UserId, string PlaylistName);
    }
}
