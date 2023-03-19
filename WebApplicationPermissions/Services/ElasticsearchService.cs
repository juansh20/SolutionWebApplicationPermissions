using Elasticsearch.Net;
using Nest;
using WebApplicationPermissions.Interfaces;
using WebApplicationPermissions.Models;

namespace WebApplicationPermissions.Services
{
    public class ElasticsearchService : IElasticsearchService
    {
        private readonly ElasticClient _client;

        public ElasticsearchService(IConfiguration configuration)
        {
            var elasticsearchUrl = configuration.GetValue<string>("ElasticsearchUrl");

            var settings = new ConnectionSettings(new Uri(elasticsearchUrl))
                .DefaultIndex("default-index-name")
                .DefaultMappingFor<Permission>(m => m
                    .IndexName("permission-index-name")
                    .PropertyName(p => p.Id, "id")
                );

            _client = new ElasticClient(settings);
        }

        public async Task InsertAsync<T>(T document) where T : class
        {
            await _client.IndexDocumentAsync(document);
        }

        public async Task UpdateAsync<T>(T document) where T : class, IHasIntId
        {
            await _client.UpdateAsync<T>(document.Id.ToString(), u => u.Doc(document));
        }

        public async Task DeleteAsync<T>(int id) where T : class, IHasIntId
        {
            await _client.DeleteAsync<T>(id);
        }

        public async Task<T> GetByIdAsync<T>(int id) where T : class, IHasIntId
        {
            var response = await _client.GetAsync<T>(id);

            if (!response.IsValid || response.Source == null)
            {
                return null;
            }

            return response.Source;
        }
    }
}
