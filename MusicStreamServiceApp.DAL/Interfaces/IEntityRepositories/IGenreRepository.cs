using MusicStreamServiceApp.DAL.Entities;
using System.Threading.Tasks;

namespace MusicStreamServiceApp.DAL.Interfaces.IEntityRepositories
{
    public interface IGenreRepository : IGenericRepository<Genre, int>
    {
        Task<Genre> GetByName(string Name);
    }
}
