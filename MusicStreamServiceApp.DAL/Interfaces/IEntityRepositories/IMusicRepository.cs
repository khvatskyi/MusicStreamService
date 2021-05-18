using System.Collections.Generic;
using System.Threading.Tasks;
using MusicStreamServiceApp.DAL.Entities;

namespace MusicStreamServiceApp.DAL.Interfaces.IEntityRepositories
{
    public interface IMusicRepository : IGenericRepository<Music, int>
    {
        Task<IEnumerable<Music>> GetByName(string Name);
        Task<Music> Get(string Name, string Author, int? Year);
        Task<ICollection<Music>> GetByAlbumId(int AlbumId);
    }
}
