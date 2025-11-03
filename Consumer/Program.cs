using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

// Creating Connection
ConnectionFactory factory = new ConnectionFactory();
factory.Uri = new Uri("...");

// active connection and channel
IConnection connection = await factory.CreateConnectionAsync();
IChannel channel = await connection.CreateChannelAsync();

/* headers exchange example
channel.ExchangeDeclareAsync(
    exchange: "headers-exchange-example",
    type: ExchangeType.Headers
    );
string queueName = channel.QueueDeclareAsync().Result.QueueName;
channel.QueueBindAsync(
    queue: queueName,
    exchange: "headers-exchange-example",
    routingKey: string.Empty,
    arguments: new Dictionary<string, object>
    {
        { "format", "pdf" },
        { "shape", "a4" },
        { "x-match", "any" } // any or all
    }
    );
AsyncEventingBasicConsumer consumer = new AsyncEventingBasicConsumer(channel);
channel.BasicConsumeAsync(
    queue: queueName,
    autoAck: true,
    consumer: consumer
    );
*/
/* topic exchange example 
channel.ExchangeDeclareAsync(
    exchange: "topic-exchange-example",
    type: ExchangeType.Topic
    );

Console.Write("Enter Topic..:");
string topic = Console.ReadLine();

string queueName = channel.QueueDeclareAsync().Result.QueueName;

channel.QueueBindAsync(
    queue: queueName,
    exchange: "topic-exchange-example",
    routingKey: topic
    );

AsyncEventingBasicConsumer consumer = new AsyncEventingBasicConsumer(channel);
channel.BasicConsumeAsync(
    queue: queueName,
    autoAck: true,
    consumer: consumer
    );
*/

/* fanout exchange example
channel.ExchangeDeclareAsync(
    exchange: "fanout-exchange-example",
    type: ExchangeType.Fanout
    );

Console.WriteLine("kuyruk adını giriniz...");
string _queueName = Console.ReadLine();

channel.QueueDeclareAsync(
    queue: _queueName,
    exclusive: false 
    );

channel.QueueBindAsync(
    _queueName,
    "fanout-exchange-example",
    string.Empty

    );

AsyncEventingBasicConsumer consumer = new AsyncEventingBasicConsumer(channel);
channel.BasicConsumeAsync(
    queue: _queueName,
    autoAck: true,
    consumer: consumer
    );

*/

/* direct exchange example
channel.ExchangeDeclareAsync(
    exchange: "direct-exchange-example",
    type: ExchangeType.Direct
    );

// get queue name
var queueName = channel.QueueDeclareAsync().Result.QueueName;

// bind queue to exchange with routing key
channel.QueueBindAsync(
    queue: queueName,
    exchange: "direct-exchange-example",
    routingKey: "direct-queue-example"
    );


AsyncEventingBasicConsumer consumer = new AsyncEventingBasicConsumer(channel);
channel.BasicConsumeAsync(
    queue: queueName,
    autoAck: true,
    consumer: consumer
    );  
*/

/*    // declare a queue
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
*/

consumer.ReceivedAsync += (sender, e) =>
{
    Console.WriteLine(Encoding.UTF8.GetString(e.Body.Span));
    return Task.CompletedTask;
};

Console.ReadLine();
