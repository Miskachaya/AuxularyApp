using NATS.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AuxularyApp.Services
{
    public interface INatsServise
    {
        Task SendAsync(string json);
    }
    public class NATSService: INatsServise
    {
        public NATSService() 
        { 
        }
        public async Task SendAsync( string json)
        {
            
            var natsClient = new NatsClient();
            MessageBox.Show(natsClient.Connection.ToString());
            _ = Task.Run(async () => 
            {
                while (true) 
                {
                    await natsClient.PublishAsync(subject:"Queue", data:json);                }
            });
        }
    }
}
