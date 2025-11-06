using MassTransit;
using Shared.Messages;

string rabbitMQUri = "...";

string queueName = "request-queue";

IBusControl bus = Bus.Factory.CreateUsingRabbitMq(factory =>
{
    factory.Host(rabbitMQUri);
});

await bus.StartAsync();

var request = bus.CreateRequestClient<RequestMessage>(new Uri($"{rabbitMQUri}/{queueName}"));


for(int i=0; i < 100; i++)
{
    var response = await request.GetResponse<ResponseMessage>(new RequestMessage()
    {
        Text = "Hello fro request :" + i,
        No= i
    });
    Console.WriteLine($"Response received : {response.Message.Text}");
}



string text = Console.ReadLine();