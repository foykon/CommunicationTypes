using MassTransit;
using Shared.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReqResPatternMassTransit.Consumer.Consumers
{
    public class RequestConsumer : IConsumer<RequestMessage>
    {
        public Task Consume(ConsumeContext<RequestMessage> context)
        {
            var message = context.Message;

            Console.WriteLine($"Message Received: {message.Text}");

            context.RespondAsync<ResponseMessage>(new ResponseMessage
            {
                Text = $"Answer to your {message.No}. message, Hello from Consumer. "
            });
            return Task.CompletedTask;
        }
    }
}
