using WebApplicationPermissions.Models;

namespace WebApplicationPermissions.Interfaces
{
    public interface IPermissionService
    {
        Task<Permission> RequestPermission(Permission permission);
        Task<Permission> ModifyPermission(Permission permission);
        Task<IEnumerable<Permission>> GetPermissions();
        Task<Permission> GetPermission(int id);
    }
}
