using Confluent.Kafka;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuxularyApp.Services
{
    public interface IKafkaService
    {
        Task<bool> SendMessageAsync(string message);
    }
    public class KafkaService : IKafkaService
    {
        private readonly IProducer<Null, string> _producer;
        private readonly string _topic; 
        private readonly string _groupId;

        public KafkaService( )
        {

            var config = new ProducerConfig
            {
                BootstrapServers = "localhost:9092",
                // Дополнительные настройки:
                Acks = Acks.All, // Гарантированная доставка
                MessageSendMaxRetries = 3,
                RetryBackoffMs = 1000
            };

            _producer = new ProducerBuilder<Null, string>(config).Build();
            
            _topic = "fistingtopic";
        }

        public async Task<bool> SendMessageAsync(string message)
        {
            try
            {
                var kafkaMessage = new Message<Null, string>
                {
                    Value = message,
                    Timestamp = new Timestamp(DateTime.UtcNow)
                };

                var deliveryResult = await _producer.ProduceAsync(_topic, kafkaMessage);

                Console.WriteLine($"Сообщение доставлено в партицию: {deliveryResult.Partition}, " +
                                $"оффсет: {deliveryResult.Offset}");
                return true;
            }
            catch (ProduceException<Null, string> ex)
            {
                Console.WriteLine($"Ошибка доставки: {ex.Error.Reason}");
                return false;
            }
        }

        public void Dispose()
        {
            _producer?.Flush(TimeSpan.FromSeconds(10));
            _producer?.Dispose();
        }
    }
}
