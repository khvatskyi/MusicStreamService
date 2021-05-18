using MongoDB.Driver;
using MusicStreamServiceApp.DAL.Interfaces;
using MusicStreamServiceApp.DAL.MongoDB.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MusicStreamServiceApp.DAL.MongoDB.Repositories
{
    public class GenericRepository<TEntity, T> : IGenericRepository<TEntity, T> where TEntity : class, IEntity<T>
    {
        protected readonly IMongoCollection<TEntity> collection;

        public GenericRepository(IMongoDBSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            collection = database.GetCollection<TEntity>(settings.CollectionName);
        }

        public async Task Add(TEntity entity)
        {
            await collection.InsertOneAsync(entity);
        }

        public async Task<bool> Any(T Id)
        {
            return await collection.FindAsync(e => e.Id.Equals(Id)) != null;
        }

        public async Task Delete(TEntity entity)
        {
            await collection.DeleteOneAsync(e => e.Id.Equals(entity.Id));
        }

        public async Task<TEntity> Get(T Id)
        {
            return await collection.Find(e => e.Id.Equals(Id)).SingleAsync();
        }

        public async Task<IEnumerable<TEntity>> GetAll()
        {
            return await collection.Find(e => true).ToListAsync();
        }

        public async Task Update(TEntity entity)
        {
            await collection.ReplaceOneAsync(e => e.Id.Equals(entity.Id), entity);
        }
    }
}
