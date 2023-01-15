using System;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Memphis.Client;
using Memphis.Client.Consumer;
using Memphis.Client.Core;
using Microsoft.Extensions.Logging;
using ProductOffer.Processor.Data;
using ProductOffer.Processor.Options;
using ProductOffer.Processor.Repository;

namespace ProductOffer.Processor.Consumer
{
    public class MessageConsumer
    {
        private readonly MemphisConsumer _memphisConsumer;
        private readonly MemphisClient _memphisClient;
        private readonly ProductOfferRepository _offerRepository;
        private readonly ILogger<MessageConsumer> _logger;

        
        public MessageConsumer(MemphisClientOptions memphisClientOptions, MemphisClient memphisClient,
            ProductOfferRepository offerRepository, ILogger<MessageConsumer> logger)
        {
            _memphisClient = memphisClient ?? throw new ArgumentNullException(nameof(memphisClient));
            _memphisConsumer = _memphisClient.CreateConsumer(new ConsumerOptions
            {
                StationName = memphisClientOptions.StationName,
                ConsumerName = "productofferprocessor",
                ConsumerGroup = "",
            }).Result;

            _offerRepository = offerRepository ?? throw new ArgumentNullException(nameof(offerRepository));
            ;
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }


        public async Task StartConsume()
        {
            EventHandler<MemphisMessageHandlerEventArgs> msgHandler = (sender, args) =>
            {
                if (args.Exception != null)
                {
                    _logger.LogError("An error occured: {}", args.Exception);
                    return;
                }


                args.MessageList.ForEach(item =>
                {
                    _logger.LogInformation("Message received...");
                    var jsonString = Encoding.UTF8.GetString(item.GetData());
                    var obj = JsonSerializer.Deserialize<ProductOfferDetails>(jsonString);

                    if (obj is null)
                    {
                        _logger.LogError("Unable to deserialize from {} to type: ProductOfferDetails", jsonString);
                        return;
                    }

                    obj.Id = Guid.NewGuid().ToString();
                    obj.CreatedAt = DateTime.UtcNow;
                    obj.UpdatedAt = obj.CreatedAt;

                    _offerRepository.CreateAsync(obj).Wait();
                    _logger.LogInformation("Message persisted with Id {}", obj.Id);
                    
                    item.Ack();
                });
            };

            await _memphisConsumer.ConsumeAsync(
                msgCallbackHandler: msgHandler,
                dlqCallbackHandler: msgHandler);
        }
    }
}