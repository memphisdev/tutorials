using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ProductOffer.Processor.Consumer;
using ProductOffer.Processor.Repository;

namespace ProductOffer.Processor.BackgroundServices
{
    public class ProductOfferProcessor : BackgroundService
    {
        private readonly ILogger<ProductOfferProcessor> _logger;
        private readonly ProductOfferRepository _offerRepository;
        private readonly MessageConsumer _messageConsumer;

        public ProductOfferProcessor(ProductOfferRepository offerRepository, MessageConsumer messageConsumer, ILogger<ProductOfferProcessor> logger)
        {
            _offerRepository = offerRepository ?? throw new ArgumentNullException(nameof(offerRepository));
            _messageConsumer = messageConsumer ?? throw new ArgumentNullException(nameof(messageConsumer));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await _messageConsumer.StartConsume();
            }
        }
    }
}