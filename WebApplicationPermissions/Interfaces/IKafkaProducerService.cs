using WebApplicationPermissions.Dtos;

namespace WebApplicationPermissions.Interfaces
{
    public interface IKafkaProducerService
    {
        Task SendMessageAsync(string topicName, KafkaMessageDto messageDto);
    }
}
