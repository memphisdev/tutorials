using System;
using System.Collections.Specialized;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Memphis.Client;
using Memphis.Client.Producer;
using Microsoft.Extensions.Logging;
using ProductOffer.WebAPI.Options;
using ProductOffer.WebAPI.Services.Commands;

namespace ProductOffer.WebAPI.Producer
{
    public class MessageProducer
    {
        private readonly MemphisProducer _memphisProducer;
        private readonly MemphisClient _memphisClient;
        private ILogger<MessageProducer> _logger;

        public MessageProducer(MemphisClientOptions memphisClientOptions, MemphisClient memphisClient,
            ILogger<MessageProducer> logger)
        {
            _memphisClient = memphisClient ?? throw new ArgumentNullException(nameof(memphisClient));
            _memphisProducer = _memphisClient.CreateProducer(
                stationName: memphisClientOptions.StationName,
                producerName: "productofferwebapi",
                generateRandomSuffix: true).Result;
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }


        public async Task PublishCommand(Models.SendProductOfferDetails command)
        {
            var commonHeaders = new NameValueCollection();
            commonHeaders.Add("UserRequestedAt", DateTime.UtcNow.ToString());

            var jsonString = JsonSerializer.Serialize(command);

            await _memphisProducer.ProduceAsync(Encoding.UTF8.GetBytes(jsonString), commonHeaders);

            _logger.LogInformation("Message sent successfully at {time}", DateTime.UtcNow);
        }
    }
}