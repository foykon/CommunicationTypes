using MassTransit;
using Shared.Messages;

string rabbitMQUri = "amqps://snqsudjh:1tc207xCDHiQUzsCJJTOcrnfaiRBCEM0@moose.rmq.cloudamqp.com/snqsudjh";

string requestQueue = "request-queue";
string responseQueue = "response-queue";

IBusControl bus = Bus.Factory.CreateUsingRabbitMq(factory =>
{
    factory.Host(rabbitMQUri);
});

ISendEndpoint sendEndpoint = await bus.GetSendEndpoint(new($"{rabbitMQUri}/{requestQueue}"));

Console.WriteLine("Press any key to publish messages :");

string text = Console.ReadLine();
string no = Guid.NewGuid().ToString();

await sendEndpoint.Send<IMessage>(new RequestMessage(){
    Text = text,
    No = no
});

Console.WriteLine("Message sent");






Console.Read();