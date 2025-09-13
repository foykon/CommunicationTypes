using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

// Creating Connection
ConnectionFactory factory = new ConnectionFactory();
factory.Uri = new Uri("...");

// active connection and channel
IConnection connection = await factory.CreateConnectionAsync();
IChannel channel = await connection.CreateChannelAsync();

// declare a queue
await channel.QueueDeclareAsync(
    queue: "example-queue",
    exclusive: false // same as publisher 
);

// reading message from mq
AsyncEventingBasicConsumer consumer = new AsyncEventingBasicConsumer(channel);
await channel.BasicConsumeAsync(
    queue: "example-queue",
    false,
    consumer
);
consumer.ReceivedAsync += (sender, e) =>
{
    // message from queue 
    // e.body : gets message from queue s all data
    // e.Body.Span or e.body.ToArray() : gets byte data
    Console.WriteLine(Encoding.UTF8.GetString(e.Body.Span));

    return Task.CompletedTask; 
};

Console.ReadLine();
