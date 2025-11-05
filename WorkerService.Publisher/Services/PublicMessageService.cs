using MassTransit;
using Microsoft.Extensions.Hosting;
using Shared.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkerService.Publisher.Services
{
    public class PublicMessageService : BackgroundService
    {
        readonly IPublishEndpoint _publishEndpoint;
        public PublicMessageService(IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
        }
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            int i = 0;

            while (true)
            {
                ExampleMessage message = new ExampleMessage
                {
                    Text = $"Message {++i} sent at {DateTime.Now}"
                };
                Task.Delay(200);
                _publishEndpoint.Publish(message);
            }
        }
    }
}
