using MusicStreamServiceApp.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MusicStreamServiceApp.DAL.Interfaces.IEntityRepositories
{
    public interface IAlbumRepository : IGenericRepository<Album, int>
    {
        Task<Album> Get(string Name, string Author);

        Task<IEnumerable<Album>> GetAuthorAlbums(string Author);
    }
}
