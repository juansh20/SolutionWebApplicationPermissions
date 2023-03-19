using WebApplicationPermissions.Interfaces;

namespace WebApplicationPermissions.Dtos
{
    public class KafkaMessageDto : IHasId
    {
        public string Id { get; set; }
        public string OperationName { get; set; }
    }
}
