using System;
using System.Reflection;
using MediatR;
using Memphis.Client;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProductOffer.WebAPI.Options;
using ProductOffer.WebAPI.Producer;
using ProductOffer.WebAPI.Repository;
using ProductOffer.WebAPI.Services.Queries;

namespace ProductOffer.WebAPI.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddApiServices(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            // serviceCollection.AddTransient<IGeolocationService, CustomGeolocationService>();
            // serviceCollection.AddTransient<IAirportProvider, CTeleportProvider>();
            serviceCollection.AddSingleton<ProductOfferRepository, ProductOfferRepository>();
            serviceCollection.AddSingleton<MessageProducer, MessageProducer>();
            return serviceCollection;
        }

        public static IServiceCollection AddApiConfiguration(this IServiceCollection serviceCollection,
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

        public static IServiceCollection AddMediatr(this IServiceCollection serviceCollection)
        {
            return serviceCollection.AddMediatR(typeof(GetProductOfferDetailsByProductIdQuery).GetTypeInfo().Assembly);
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