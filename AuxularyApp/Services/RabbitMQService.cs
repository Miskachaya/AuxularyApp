using AuxularyApp.Models.DataModels.InstructionModels;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;

namespace AuxularyApp.Services
{

    public interface IRabbitMQService
    {
        Task DataReceivedEventArgs(ObservableCollection<Instruction> InstructionCollection, ObservableCollection<Instruction> PlannedInstructionCollection);
        Task DataSendEventArgs(string json);
    }
    public class RabbitMQService() : IRabbitMQService
    {
        public async Task DataSendEventArgs( string json )
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using var connection = await factory.CreateConnectionAsync();
            using var channel = await connection.CreateChannelAsync();
            {
                // Создание очереди (если её нет)
                await channel.QueueDeclareAsync(queue: "instructionQueue",
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                // Сообщение для отправки
                string message = json;
                var body = Encoding.UTF8.GetBytes(message);

                // Отправка сообщения
                await channel.BasicPublishAsync(exchange: string.Empty, routingKey: "instructionQueue", body: body);
            }
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
            consumer.ReceivedAsync += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                MessageBox.Show(message);
                Instruction? instruction = JsonSerializer.Deserialize<Instruction>(message);

                InstructionCollection?.Add(instruction);
                PlannedInstructionCollection?.Add(instruction);
                return Task.CompletedTask;
            };
            await channel.BasicConsumeAsync("instructionQueue", autoAck: true, consumer: consumer);
            channel.Dispose();
        }
    }
}
