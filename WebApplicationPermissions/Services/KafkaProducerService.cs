using Confluent.Kafka;
using WebApplicationPermissions.Dtos;
using WebApplicationPermissions.Interfaces;
using System.Text.Json;
using System.Text.Json.Serialization;
using WebApplicationPermissions.Utils;

namespace WebApplicationPermissions.Services
{
    public class KafkaProducerService : IKafkaProducerService
    {
        private readonly ProducerConfig _producerConfig;

        public KafkaProducerService(string bootstrapServers)
        {
            _producerConfig = new ProducerConfig
            {
                BootstrapServers = bootstrapServers
            };
        }

        public async Task SendMessageAsync(string topicName, KafkaMessageDto messageDto)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };

            using var producer = new ProducerBuilder<Null, KafkaMessageDto>(_producerConfig)
                .SetValueSerializer(new MyJsonSerializer<KafkaMessageDto>(options))
                .Build();

            var kafkaMessage = new Message<Null, KafkaMessageDto>
            {
                Value = messageDto
            };

            await producer.ProduceAsync(topicName, kafkaMessage);
        }
    }
}