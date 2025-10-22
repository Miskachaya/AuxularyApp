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
    }
    public class KafkaService : IKafkaService
    {
        private IConsumer<Ignore, string>? _consumer;

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
        public async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            StartAsync(stoppingToken);
            //await Task.Yield(); // Ensures method runs asynchronously

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    var result = _consumer?.Consume(stoppingToken);
                    if (result == null || string.IsNullOrWhiteSpace(result.Message?.Value))
                        continue;
                    MessageBox.Show($"message {result.Message}\n value {result.Value}\n message value {result.Message.Value}");
                } catch (Exception ex) { MessageBox.Show("ошибка: " + ex.Message); }  
            }
        }
    }
    }
