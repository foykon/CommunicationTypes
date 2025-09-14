using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

// Creating Connection
ConnectionFactory factory = new ConnectionFactory();
factory.Uri = new Uri("amqps://snqsudjh:1tc207xCDHiQUzsCJJTOcrnfaiRBCEM0@moose.rmq.cloudamqp.com/snqsudjh");

// active connection and channel
IConnection connection = await factory.CreateConnectionAsync();
IChannel channel = await connection.CreateChannelAsync();

// declare a queue
await channel.QueueDeclareAsync(
    queue: "example-queue",
    exclusive: false, // same as publisher 
        durable: true

);

// reading message from mq
AsyncEventingBasicConsumer consumer = new AsyncEventingBasicConsumer(channel);
await channel.BasicConsumeAsync(
    queue: "example-queue",
    false,
    consumer
);
channel.BasicQosAsync(0, 1, false);
consumer.ReceivedAsync += (sender, e) =>
{
    // message from queue 
    // e.body : gets message from queue s all data
    // e.Body.Span or e.body.ToArray() : gets byte data
    Console.WriteLine(Encoding.UTF8.GetString(e.Body.Span));

    // success feedback to delete from queue
    channel.BasicAckAsync(e.DeliveryTag, multiple: false);

    // to add queue again
    //channel.BasicNackAsync(e.DeliveryTag, multiple: false,true);

    // BasicCancel rejects all queue, BasicReject only rejects the message 

    return Task.CompletedTask; 
};

Console.ReadLine();
