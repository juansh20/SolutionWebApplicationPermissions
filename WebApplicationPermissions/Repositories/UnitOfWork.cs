using WebApplicationPermissions.Context;
using WebApplicationPermissions.Interfaces;

namespace WebApplicationPermissions.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DefaultContext _dbContext;

        public UnitOfWork(DefaultContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }


    }
}
