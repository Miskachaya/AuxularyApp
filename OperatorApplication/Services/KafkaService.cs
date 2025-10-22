using Confluent.Kafka;
using OperatorApplication.Models.DataModels.InstructionModels;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;

namespace OperatorApplication.Services
{

    public interface IKafkaService
    {
        void SetCollectionUpdater(Action<Instruction> updater);
        Task StartConsumingAsync(Func<string, Task> messageHandler);
    }
        public class KafkaService : IKafkaService
        {
            public  IConsumer<Ignore, string> _consumer;
           public  CancellationTokenSource _cancellationTokenSource;
            private Action<Instruction> _collectionUpdater;
            private async Task OnMessageReceived(object model, string _message)
            {
                var message = _message.ToString();
                Instruction? instruction = JsonSerializer.Deserialize<Instruction>(message);
                MessageBox.Show("Создан объект");
                // Вызываем метод ViewModel для обновления коллекций
                _collectionUpdater?.Invoke(instruction);
            }
            public void SetCollectionUpdater(Action<Instruction> updater)
            {
                _collectionUpdater = updater;
            }
            public KafkaService()
            {
               
            }
            public async Task StartConsumingAsync(Func<string, Task> messageHandler)
            {
                var config = new ConsumerConfig
                {
                    BootstrapServers = "localhost:9092",
                    GroupId = "group1",
                    AutoOffsetReset = AutoOffsetReset.Earliest,
                    EnableAutoCommit = true
                };
                _consumer = new ConsumerBuilder<Ignore, string>(config)
                    .Build();
                _consumer.Subscribe("fistingtopic");
                _cancellationTokenSource = new CancellationTokenSource();
            while (!_cancellationTokenSource.IsCancellationRequested)
            {
                var consumerResult = _consumer.Consume(_cancellationTokenSource.Token);
                if (consumerResult?.Message != null)
                {
                    MessageBox.Show("Сообщение получен");
                    await messageHandler(consumerResult.Message.Value);
                    _consumer.StoreOffset(consumerResult);
                    _consumer.Commit(consumerResult);
                    OnMessageReceived(new object(), consumerResult.Message.Value);
                }else
                {
                    continue;
                }

            }
            }
        }
    }
