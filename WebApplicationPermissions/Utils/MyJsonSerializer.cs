using Confluent.Kafka;
using System.Text;
using System.Text.Json;

namespace WebApplicationPermissions.Utils
{
    public class MyJsonSerializer<T> : ISerializer<T>
    {
        private readonly JsonSerializerOptions _options;

        public MyJsonSerializer(JsonSerializerOptions options)
        {
            _options = options;
        }

        public byte[] Serialize(T data, SerializationContext context)
        {
            var jsonString = JsonSerializer.Serialize(data, _options);
            return Encoding.UTF8.GetBytes(jsonString);
        }
    }
}
