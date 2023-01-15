using System.Collections.Generic;
using MediatR;

namespace ProductOffer.WebAPI.Services.Queries
{
    public sealed record GetProductOfferDetailsQuery() : IRequest<List<Models.ProductOfferDetails>>;

    public sealed record GetProductOfferDetailsByProductIdQuery(string productId) : IRequest<Models.ProductOfferDetails>;
}