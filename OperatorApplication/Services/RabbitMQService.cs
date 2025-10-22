using Microsoft.Extensions.Logging;
using OperatorApplication.Models.DataModels.InstructionModels;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
namespace OperatorApplication.Services
{
    public interface IRabbitMQService
    {
        Task DataReceivedEventArgs(ObservableCollection<Instruction> InstructionCollection, ObservableCollection<Instruction> PlannedInstructionCollection);
        public void SetCollectionUpdater(Action<Instruction> updater);
    }
    public class RabbitMQService : IRabbitMQService
    {
        private Action<Instruction> _collectionUpdater;

        public void SetCollectionUpdater(Action<Instruction> updater)
        {
            _collectionUpdater = updater;
        }

        private async Task OnMessageReceived(object model, BasicDeliverEventArgs ea)
        {
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            Instruction? instruction = JsonSerializer.Deserialize<Instruction>(message);
            
            // Вызываем метод ViewModel для обновления коллекций
            _collectionUpdater?.Invoke(instruction);
        }

        public RabbitMQService()
        {

        }
        public async Task DataReceivedEventArgs(ObservableCollection<Instruction> InstructionCollection, ObservableCollection<Instruction> PlannedInstructionCollection)
        {
            var tcs = new TaskCompletionSource<Instruction>();
            var factory = new ConnectionFactory { HostName = "localhost" };
            var connection = await factory.CreateConnectionAsync();
            var channel = await connection.CreateChannelAsync();

            await channel.QueueDeclareAsync(queue: "instructionQueue",
                                        durable: false,
                                        exclusive: false,
                                        autoDelete: false,
                                        arguments: null);
            var consumer = new AsyncEventingBasicConsumer(channel);
            consumer.ReceivedAsync += OnMessageReceived;
            await channel.BasicConsumeAsync("instructionQueue", autoAck: true, consumer: consumer);
        }
    }
} 

