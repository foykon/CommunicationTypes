using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;

class Consumer
{
    static void Main()
    {
        ConnectionFactory factory = new();
        factory.Uri = new Uri("...");

        using IConnection connection = factory.CreateConnection();
        using IModel channel = connection.CreateModel();

        channel.ExchangeDeclare(exchange: "direct-exchange-example", type: ExchangeType.Direct);

        string queueName = channel.QueueDeclare().QueueName;

        channel.QueueBind(
            queue: queueName,
            exchange: "direct-exchange-example",
            routingKey: "direct-queue-example"
        );

        EventingBasicConsumer consumer = new(channel);
        channel.BasicConsume(
            queue: queueName,
            autoAck: true,
            consumer: consumer
        );

        consumer.Received += (sender, e) =>
        {
            string message = Encoding.UTF8.GetString(e.Body.Span);
            Console.WriteLine(message);
        };

        Console.ReadLine();
    }
}
