using MassTransit;
using Shared.Messages;

string rabbitMQUri = "...";

string queueName = "sample-queue";

IBusControl bus = Bus.Factory.CreateUsingRabbitMq(factory =>
{
    factory.Host(rabbitMQUri);
});

// use send endpoint to send messages to specific queue
ISendEndpoint sendEndpoint = await bus.GetSendEndpoint(new($"{rabbitMQUri}/{queueName}"));

Console.WriteLine("Press any key to publish messages :");

string text = Console.ReadLine();

await sendEndpoint.Send<IMessage>(new ExampleMessage(){
    Text = text
});

Console.Read();