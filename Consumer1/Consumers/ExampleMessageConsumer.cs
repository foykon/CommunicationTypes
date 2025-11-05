using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Shared.Messages;
using System.Threading.Tasks;

namespace Consumer1.Consumers
{
    public class ExampleMessageConsumer : IConsumer<IMessage>
    {
        public Task Consume(ConsumeContext<IMessage> context)
        {
            Console.WriteLine($"Message Received: {context.Message.Text}");
            return Task.CompletedTask;
        }
    }
}
