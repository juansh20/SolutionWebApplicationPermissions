namespace WebApplicationPermissions.Interfaces
{
    public interface IElasticsearchService
    {
        Task InsertAsync<T>(T document) where T : class;
        Task UpdateAsync<T>(T document) where T : class, IHasIntId;
        Task DeleteAsync<T>(int id) where T : class, IHasIntId;
        Task<T> GetByIdAsync<T>(int id) where T : class, IHasIntId;
    }

    public interface IHasId
    {
        string Id { get; set; }
    }

    public interface IHasIntId
    {
        int Id { get; set; }
    }

}
