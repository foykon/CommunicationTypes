using RabbitMQ.Client;
using System.Text;

// creating connection
ConnectionFactory factory  = new ConnectionFactory();
factory.Uri = new Uri("...");

// active connection and channel
using IConnection connection = await factory.CreateConnectionAsync(); 
using IChannel channel = await connection.CreateChannelAsync();

/* headers exchange example 
channel.ExchangeDeclareAsync(
    exchange: "headers-exchange-example",
    type: ExchangeType.Headers
    );

for (int i = 0; i < 100; i++)
{
    await Task.Delay(200);
    byte[] byteMessage = Encoding.UTF8.GetBytes("Hello from RabbitMQ .NET 6 Client! " + i);

    Console.WriteLine("Enter Headers..:");
    string headersInput = Console.ReadLine();

// x-match hangi özelliklere göre filtreleme yapacağımızı belirler any veya all olabilir any: verilen özelliklerden herhangi biri eşleştiğinde mesajı alır all: verilen tüm özellikler eşleştiğinde mesajı alır
    channel.BasicPublishAsync(
        exchange: "headers-exchange-example",
        routingKey: string.Empty,
        basicProperties: new BasicProperties
        {
            Headers = new Dictionary<string, object>
            {
                { "format", headersInput },
                { "shape", "a4" },
                { "x-match", "any" } // any or all
            }
        }, body: byteMessage


       );
}
*/





/* topic exchange example 
channel.ExchangeDeclareAsync(
    exchange: "topic-exchange-example",
    type: ExchangeType.Topic
    );

for(int i=0; i<100; i++)
{
    await Task.Delay(200);
    byte[] byteMessage = Encoding.UTF8.GetBytes("Hello from RabbitMQ .NET 6 Client! " + i);

    Console.Write("Enter Topic..:");

    string topic = Console.ReadLine();

    channel.BasicPublishAsync(
        exchange: "topic-exchange-example",
        routingKey: topic,
        body: byteMessage
        );

}
*/

/* usage of * and #
*.weather: Bu ifade, herhangi bir kelime ile başlayan ve ardından “weather” ile biten routing key değerlerini temsil eder. Örneğin, “usa.weather” veya “europe.weather” gibi.

#.news: Bu ifade, herhangi bir sayıda kelime ile başlayan ve ardından “news” ile biten routing key değerlerini temsil eder. Örneğin, “usa.news”, “europe.news” veya "xyz.abc.news” gibi.

Aslında her ikisi de aynı şey fakat #. karakterinde birden fazla kelimeyi temsil ederken *. karakteri tek bir kelimeyi temsil ediyor.
*/

/* fanout exchange example
channel.ExchangeDeclareAsync(
    exchange: "fanout-exchange-example",
    type: ExchangeType.Fanout
    );

for (int i = 0; i < 100; i++)
{
    await Task.Delay(200);
    byte[] byteMessage = Encoding.UTF8.GetBytes("Hello from RabbitMQ .NET 6 Client! " + i);

    channel.BasicPublishAsync(
    exchange: "fanout-exchange-example",
    routingKey: "", // string.empty for fanout exchange
    body: byteMessage
    );
}
*/

/* direct exchange example
channel.ExchangeDeclareAsync(
    exchange: "direct-exchange-example",
    type: ExchangeType.Direct
    );


while (true)
{
    Console.Write("Enter your message: ");
    string message = Console.ReadLine();
    byte[] byteMessage = Encoding.UTF8.GetBytes(message);
    channel.BasicPublishAsync(
        exchange: "direct-exchange-example",
        routingKey: "direct-queue-example",
        body: byteMessage
        );
}
*/

/*  exmple queue publish and declare
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
*/

Console.Read();