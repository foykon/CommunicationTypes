using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MassTransit;
using WorkerService.Consumer.Consumers;
using Microsoft.Extensions.Logging;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureLogging(logging =>
    {
        logging.ClearProviders();
        logging.AddConsole();
    })
    .ConfigureServices(services =>
    {
        services.AddMassTransit(configurator =>
        {
            configurator.AddConsumer<ExampleMessageConsumer>();

            configurator.UsingRabbitMq((context, rabbitMqCfg) =>
            {
                rabbitMqCfg.Host("...");

                // consumer has to listen a queue
                // publisher sends all 
                rabbitMqCfg.ReceiveEndpoint("example-message-queue", endpoint =>
                {
                    endpoint.ConfigureConsumer<ExampleMessageConsumer>(context);
                });
            });

        });
    })
    .Build();

host.Run();