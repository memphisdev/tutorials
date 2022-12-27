using System;
using Memphis.Client;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProductOffer.Processor.Consumer;
using ProductOffer.Processor.Options;
using ProductOffer.Processor.Repository;

namespace ProductOffer.Processor.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddWorkerDependentServices(this IServiceCollection serviceCollection)
        {
            // serviceCollection.AddTransient<IGeolocationService, CustomGeolocationService>();
            // serviceCollection.AddTransient<IAirportProvider, CTeleportProvider>();
            serviceCollection.AddSingleton<ProductOfferRepository, ProductOfferRepository>();
            serviceCollection.AddSingleton<MessageConsumer, MessageConsumer>();

            return serviceCollection;
        }

        public static IServiceCollection AddWorkerConfiguration(this IServiceCollection serviceCollection,
            IConfiguration configuration)
        {
            return serviceCollection;
        }

        public static IServiceCollection AddInfraConfiguration(this IServiceCollection serviceCollection,
            IConfiguration configuration)
        {
            var offerDbOptions = new ProductOfferDbOptions()
            {
                DatabaseName = Environment.GetEnvironmentVariable("APP_DB_NAME"),
                ConnectionString = Environment.GetEnvironmentVariable("APP_DB_CONNECTION_STRING"),
                ProductOfferCollectionName = Environment.GetEnvironmentVariable("APP_DB_COLLECTION_NAME"),
            };
            serviceCollection.AddSingleton<ProductOfferDbOptions>(offerDbOptions);

            var memphisClientOpts = new MemphisClientOptions()
            {
                Host = Environment.GetEnvironmentVariable("MEMPHIS_HOST"),
                Username = Environment.GetEnvironmentVariable("MEMPHIS_USERNAME"),
                Token = Environment.GetEnvironmentVariable("MEMPHIS_CONNECTION_TOKEN"),
                StationName = Environment.GetEnvironmentVariable("MEMPHIS_STATION_NAME"),
            };
            serviceCollection.AddSingleton<MemphisClientOptions>(memphisClientOpts);

            serviceCollection.AddSingleton<MemphisClient>(createMemphisClient(memphisClientOpts));
            
            return serviceCollection;
        }
        
        private static MemphisClient createMemphisClient(MemphisClientOptions memphisClientOptions)
        {
            var options = MemphisClientFactory.GetDefaultOptions();
            options.Host = memphisClientOptions.Host;
            options.Username = memphisClientOptions.Username;
            options.ConnectionToken = memphisClientOptions.Token;
            return MemphisClientFactory.CreateClient(options);
        }
    }
}