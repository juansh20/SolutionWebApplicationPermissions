using WebApplicationPermissions.Context;
using WebApplicationPermissions.Interfaces;
using WebApplicationPermissions.Models;

namespace WebApplicationPermissions.Repositories
{
    public class PermissionTypeRepository : RepositoryBase<PermissionType>, IPermissionTypeRepository
    {
        public PermissionTypeRepository(DefaultContext dbContext)
            : base(dbContext)
        {
        }
    }
}
