using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MassTransit;
using WorkerService.Publisher.Services;
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
            configurator.UsingRabbitMq((context, rabbitMqCfg) =>
            {
                rabbitMqCfg.Host("...");
            });
        });        
        services.AddHostedService<PublicMessageService>();

        //services.AddHostedService<PublicMessageService>(ProviderAliasAttribute =>
        //{
        //    using IServiceScope scope = ProviderAliasAttribute.CreateScope();
        //    IPublishEndpoint publishEndpoint  = scope.ServiceProvider.GetService<IPublishEndpoint>();
        //    return new (publishEndpoint);
        //});
    })
    .Build();

host.Run();