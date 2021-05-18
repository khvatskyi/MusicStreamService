using MusicStreamServiceApp.DAL.Entities;
using MusicStreamServiceApp.DAL.Interfaces.IEntityRepositories;
using MusicStreamServiceApp.DAL.MongoDB.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace MusicStreamServiceApp.DAL.MongoDB.Repositories
{
    public class MusicRepository : GenericRepository<Music, int>, IMusicRepository
    {
        public MusicRepository(IMongoDBSettings settings)
            : base(settings)
        {
        }

        public async Task<Music> Get(string Name, string Author, int? Year)
        {
            var builder = Builders<Music>.Filter;
            var filter = builder.Eq(nameof(Music.Name), Name) & builder.Eq(nameof(Music.Author), Author) & builder.Eq(nameof(Music.Year), Year);

            return await collection.Find(filter).FirstAsync();
        }

        public async Task<ICollection<Music>> GetByAlbumId(int AlbumId)
        {
            return await collection.Find(e => e.AlbumId.Equals(AlbumId)).ToListAsync();
        }

        public async Task<IEnumerable<Music>> GetByName(string Name)
        {
            return await collection.Find(e => e.Name.Equals(Name)).ToListAsync();
        }
    }
}
