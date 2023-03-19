using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq.Expressions;
using WebApplicationPermissions.Context;
using WebApplicationPermissions.Interfaces;

namespace WebApplicationPermissions.Repositories
{
    public class RepositoryBase<TEntity> : IRepository<TEntity> where TEntity : class,IHasIntId
    {
        private readonly DefaultContext _dbContext;
        private readonly DbSet<TEntity> _dbSet;

        public RepositoryBase(DefaultContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<TEntity>();
        }

        public async Task<TEntity> GetByIdAsync(int id, string includeProperties = null)
        {
            IQueryable<TEntity> query = _dbContext.Set<TEntity>();

            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties.Split
                    (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }
            }

            return await query.FirstOrDefaultAsync(entity => entity.Id == id);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(string includeProperties)
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<EntityEntry<TEntity>> Add(TEntity entity)
        {
            var Result = await _dbContext.AddAsync(entity);
            return Result;
        }

        public void Update(TEntity entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
        }

        public void Remove(TEntity entity)
        {
            _dbSet.Remove(entity);
        }

        public async Task<IEnumerable<TEntity>> GetAllWithIncludesAsync(params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> query = _dbContext.Set<TEntity>();

            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }

            return await query.ToListAsync();
        }

    }
}
