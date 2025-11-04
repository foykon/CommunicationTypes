using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

// creating connection
ConnectionFactory factory  = new ConnectionFactory();
factory.Uri = new Uri("...");

// active connection and channel
using IConnection connection = await factory.CreateConnectionAsync(); 
using IChannel channel = await connection.CreateChannelAsync();

string reqQueueName = "example-req-res-queue";

channel.QueueDeclareAsync(
    queue: reqQueueName,
    exclusive: false,
    durable: false,
    autoDelete: false
    );

string resQueueName = channel.QueueDeclareAsync().Result.QueueName;

var correlationId = Guid.NewGuid().ToString();
var props = new BasicProperties();
props.CorrelationId = correlationId;
props.ReplyTo = resQueueName;

for(int i = 0; i < 100; i++)
{
    byte[] byteMessage = Encoding.UTF8.GetBytes("Hello Req-Res " + i);
    channel.BasicPublishAsync(
        exchange: "",
        routingKey: reqQueueName,
        basicProperties: props,
        body: byteMessage,
        mandatory: false // if true and no queue is bound to the routing key, the message will be returned to the sender
        );
}

AsyncEventingBasicConsumer  consumer = new AsyncEventingBasicConsumer(channel);

channel.BasicConsumeAsync(
    queue: resQueueName,
    autoAck: true,
    consumer: consumer
    );

consumer.ReceivedAsync += (sender, e) =>
{
    if(e.BasicProperties.CorrelationId == correlationId)
    {
        Console.WriteLine("Response : " + Encoding.UTF8.GetString(e.Body.Span));
    }
    return Task.CompletedTask;

};


/* Work Queue Example
string queueName = "example-work-queue";

channel.QueueDeclareAsync(
    queue: queueName,
    durable: false,
    exclusive: false,
    autoDelete: false
    );

for(int i = 0; i < 100; i++)
{
    byte[] byteMessage = Encoding.UTF8.GetBytes("Hello Work Queue " + i);
    channel.BasicPublishAsync(
        exchange: "",
        routingKey: queueName,
        body: byteMessage
        );
}
*/

/* Pub-Sub Example
string exchangeName = "example-pub-sub-exchange";

channel.ExchangeDeclareAsync(
    exchange: exchangeName,
    type: ExchangeType.Fanout
    );
for (int i = 0; i < 100; i++)
{
    await Task.Delay(200);
    byte[] byteMessage = Encoding.UTF8.GetBytes("Hello subs " + i);
    channel.BasicPublishAsync(
    exchange: exchangeName,
    routingKey: "",
    body: byteMessage
    );
}
*/

/* P2P Example 
string queueName = "example-p2p-queue";

await channel.QueueDeclareAsync(
    queue: queueName,
    exclusive: false,
    durable: false,
    autoDelete: false
    );

byte[] message = Encoding.UTF8.GetBytes("Hello P2P");

await channel.BasicPublishAsync(
    exchange: "",
    routingKey: queueName,
    body: message
    );
*/

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