namespace WebApplicationPermissions.Interfaces
{
    public interface IUnitOfWork
    {
        Task<int> SaveChangesAsync();
    }
}
