using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ProductOffer.WebAPI.Repository;
using ProductOffer.WebAPI.Services.Queries;

namespace ProductOffer.WebAPI.Services.Handlers
{
    public class GetProductOffersDetailsByProductIdQueryHandler : IRequestHandler<GetProductOfferDetailsByProductIdQuery, Models.ProductOfferDetails>
    {
        private readonly ProductOfferRepository _offerRepository;

        
        public GetProductOffersDetailsByProductIdQueryHandler(ProductOfferRepository offerRepository)
        {
            _offerRepository = offerRepository ?? throw new ArgumentNullException(nameof(offerRepository));
        }

        
        public async Task<Models.ProductOfferDetails> Handle(GetProductOfferDetailsByProductIdQuery request,
            CancellationToken cancellationToken)
        {
            var result = await _offerRepository.GetAsync(productId: request.productId);

            if (result is null)
            {
                return new Models.ProductOfferDetails();
            }

            return new Models.ProductOfferDetails()
            {
                Id = result.Id,
                ProductName = result.ProductName,
                ProductId = result.ProductId,
                Currency = result.Currency,
                ProductPrice = result.ProductPrice,
            };
        }
    }
}