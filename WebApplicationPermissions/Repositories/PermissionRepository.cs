using System.Linq.Expressions;
using WebApplicationPermissions.Context;
using WebApplicationPermissions.Interfaces;
using WebApplicationPermissions.Models;

namespace WebApplicationPermissions.Repositories
{
    public class PermissionRepository : RepositoryBase<Permission>, IPermissionRepository
    {
        public PermissionRepository(DefaultContext dbContext)
            : base(dbContext)
        {
        }

    }
}
