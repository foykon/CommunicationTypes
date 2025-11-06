using MassTransit;
using ReqResPatternMassTransit.Consumer.Consumers;
using Shared.Messages;

string rabbitMQUri = "...";


string queueName = "request-queue";

IBusControl bus = Bus.Factory.CreateUsingRabbitMq(factory =>
{
    factory.Host(rabbitMQUri);
    factory.ReceiveEndpoint(queueName, endpoint =>
    {
        endpoint.Consumer<RequestConsumer>();
    });
});

await bus.StartAsync();

Console.Read();