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
        Task StartAsync(CancellationToken cancellationToken);
        Task ExecuteAsync(CancellationToken stoppingToken);
        public void SetCollectionUpdater(Action<Instruction> updater);
    }
    public class KafkaService : IKafkaService
    {
        private Action<Instruction> _collectionUpdater;
        private IConsumer<Ignore, string>? _consumer;
        public void SetCollectionUpdater(Action<Instruction> updater)
        {
            _collectionUpdater = updater;
        }
        public Task StartAsync(CancellationToken cancellationToken)
        {
            var config = new ConsumerConfig
            {
                BootstrapServers = "localhost:9092",
                GroupId = "group1",
                AutoOffsetReset = AutoOffsetReset.Earliest,
                EnableAutoCommit = true

            };

            _consumer = new ConsumerBuilder<Ignore, string>(config).Build();
            _consumer.Subscribe("fistingtopic");
            return Task.CompletedTask;
        }

        private void OnMessageReceived(string message)
        {
            Instruction? instruction = JsonSerializer.Deserialize<Instruction>(message);
            // Вызываем метод ViewModel для обновления коллекций
            _collectionUpdater?.Invoke(instruction);
        }

        public async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await Task.Yield(); // Ensures method runs asynchronously
            StartAsync(stoppingToken);
            while (!stoppingToken.IsCancellationRequested)
            {
                //MessageBox.Show("Начаало опроса");
                if (stoppingToken.IsCancellationRequested) MessageBox.Show("stoppentoken is cancel");
                var result = _consumer?.Consume(stoppingToken);
                if (result == null || string.IsNullOrWhiteSpace(result.Message?.Value))
                {
                    MessageBox.Show("Сообщение не получено");
                    continue;
                }
                        
                if(result!=null || result.Message?.Value != null)
                {
                    OnMessageReceived(result.Message.Value);
                    MessageBox.Show($"message {result.Message}\n value {result.Value}\n message value {result.Message.Value}");
                }
                Task.Delay(1000);
            }
        }
    }
    }
