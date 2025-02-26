using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MusicStreamServiceApp.DAL.Interfaces
{
    public interface IGenericRepository<TEntity, T> where TEntity : class, IEntity<T>
    {
        Task Add(TEntity entity);
        Task<bool> Any(T Id);
        Task Delete(TEntity entity);
        Task<TEntity> Get(T Id);
        Task<IEnumerable<TEntity>> GetAll();
        Task Update(TEntity entity);
    }
}
