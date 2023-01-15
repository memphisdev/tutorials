using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using ProductOffer.WebAPI.Producer;
using ProductOffer.WebAPI.Services.Commands;

namespace ProductOffer.WebAPI.Services.Handlers
{
    public class SendProductOfferDetailsCommandHandler : IRequestHandler<SendProductOfferDetailsCommand, Unit>
    {
        private readonly MessageProducer _messageProducer;
        private readonly ILogger<SendProductOfferDetailsCommandHandler> _logger;
        
        
        public SendProductOfferDetailsCommandHandler(MessageProducer messageProducer, ILogger<SendProductOfferDetailsCommandHandler> logger)
        {
            _messageProducer = messageProducer ?? throw new ArgumentNullException(nameof(messageProducer));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        
        public async Task<Unit> Handle(SendProductOfferDetailsCommand request, CancellationToken cancellationToken)
        {
            await _messageProducer.PublishCommand(request.SendProductOfferDetails);
            return Unit.Value;
        }
    }
}