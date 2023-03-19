namespace WebApplicationPermissions.Utils
{
    public class PermissionNotFoundException : Exception
    {
        public PermissionNotFoundException(int id)
        : base($"Permission with id {id} was not found.")
        {
        }
    }
}
