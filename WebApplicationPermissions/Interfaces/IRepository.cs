using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq.Expressions;

namespace WebApplicationPermissions.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task<TEntity> GetByIdAsync(int id, string includeProperties = null);
        Task<IEnumerable<TEntity>> GetAllAsync(string includeProperties);
        Task<EntityEntry<TEntity>> Add(TEntity entity);
        void Update(TEntity entity);
        void Remove(TEntity entity);
        Task<IEnumerable<TEntity>> GetAllWithIncludesAsync(params Expression<Func<TEntity, object>>[] includeProperties);
    }
}
