using MusicStreamServiceApp.DAL.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MusicStreamServiceApp.DAL.Interfaces.IEntityRepositories
{
    public interface IMusicGenreRepository : IGenericRepository<MusicGenre, int>
    {
        Task<ICollection<MusicGenre>> GetByMusicId(int MusicId);
    }
}
