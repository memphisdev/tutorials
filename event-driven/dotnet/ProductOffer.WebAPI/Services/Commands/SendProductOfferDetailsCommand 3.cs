using MediatR;
using ProductOffer.WebAPI.Models;

namespace ProductOffer.WebAPI.Services.Commands
{
    public record SendProductOfferDetailsCommand(SendProductOfferDetails SendProductOfferDetails) : IRequest;
}