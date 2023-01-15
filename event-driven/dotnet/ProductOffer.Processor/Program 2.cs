using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ProductOffer.Processor.BackgroundServices;
using ProductOffer.Processor.Extensions;

var configuration = new ConfigurationBuilder()
    .AddEnvironmentVariables()
    .AddCommandLine(args)
    .AddJsonFile("appsettings.json")
    .Build();

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration(builder =>
    {
        builder.Sources.Clear();
        builder.AddConfiguration(configuration);
    })
    .ConfigureServices(services =>
    {
        services.AddWorkerDependentServices();
        
        //read settings for worker configuration
        services.AddWorkerConfiguration(configuration);
        
        //read settings for infra configuration
        services.AddInfraConfiguration(configuration);

        services.AddHostedService<ProductOfferProcessor>();
    })
    .Build();

await host.RunAsync();