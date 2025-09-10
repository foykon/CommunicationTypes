using Grpc.Net.Client;
using Service3;

var channel = GrpcChannel.ForAddress("https://localhost:7115");
var client = new GreetService.GreetServiceClient(channel);
var response = client.SayHello(new HelloRequest { Name = Console.ReadLine() });
await Task.Run(async () =>
{
    while (await response.ResponseStream.MoveNext(new CancellationToken()))
    {
        Console.WriteLine("Gelen mesaj" + response.ResponseStream.Current.Message);
    }
});
