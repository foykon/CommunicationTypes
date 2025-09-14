﻿using RabbitMQ.Client;
using System.Text;

// creating connection
ConnectionFactory factory  = new ConnectionFactory();
factory.Uri = new Uri("...");

// active connection and channel
using IConnection connection = await factory.CreateConnectionAsync(); 
using IChannel channel = await connection.CreateChannelAsync();

// declare a queue
await channel.QueueDeclareAsync(
    queue: "example-queue",
    exclusive: false,
    durable: true
);

// publish a message
// rabbitmq accepts byte arrays, so we need to convert a string message to a byte array
//byte[] message = Encoding.UTF8.GetBytes("Hello from RabbitMQ .NET 6 Client!");
//await channel.BasicPublishAsync(
//    exchange: "",
//    routingKey: "example-queue",
//    body: message
//);


for (int i =0; i<100; i++)
{
    await Task.Delay(500);
    Console.WriteLine(i);

    var props = new BasicProperties
    {
        Persistent = true,
        ContentType = "text/plain"
    };

    byte[] message = Encoding.UTF8.GetBytes("Hello from RabbitMQ .NET 6 Client! " + i);
    await channel.BasicPublishAsync(
        exchange: "",
        routingKey: "example-queue",
        mandatory: true,
        basicProperties: props,
        body: message
    );
}


Console.Read();