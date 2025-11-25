using Confluent.Kafka;
using NATS.Client.Core;
using NATS.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace OperatorApplication.Services
{
    public interface INatsService
    {
        Task ReceiveAsync();
    }
    public class NATSService : INatsService
    {
        public async Task ReceiveAsync()
        {
            await using var natsClient = new NatsClient();

            _ = Task.Run(async () =>
            {
                // Wait for messages on the GBPUSD subject and write them to the console
                await foreach (NatsMsg<double> msg in natsClient.SubscribeAsync<double>("Queue"))
                {
                    MessageBox.Show($"New exchange rate.");
                }
            });
        }
    }
}
