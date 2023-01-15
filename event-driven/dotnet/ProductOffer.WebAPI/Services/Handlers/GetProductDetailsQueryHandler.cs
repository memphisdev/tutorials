using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ProductOffer.WebAPI.Models;
using ProductOffer.WebAPI.Repository;
using ProductOffer.WebAPI.Services.Queries;

namespace ProductOffer.WebAPI.Services.Handlers
{
    public class
        GetProductOfferDetailsQueryHandler : IRequestHandler<GetProductOfferDetailsQuery,
            List<Models.ProductOfferDetails>>
    {
        private readonly ProductOfferRepository _offerRepository;

        public GetProductOfferDetailsQueryHandler(ProductOfferRepository offerRepository)
        {
            _offerRepository = offerRepository ?? throw new ArgumentNullException(nameof(offerRepository));
        }

        public async Task<List<ProductOfferDetails>> Handle(GetProductOfferDetailsQuery request,
            CancellationToken cancellationToken)
        {
            var result = await _offerRepository.GetAsync();

            return result.Select(item => new Models.ProductOfferDetails()
            {
                Id = item.Id,
                ProductName = item.ProductName,
                ProductId = item.ProductId,
                Currency = item.Currency,
                ProductPrice = item.ProductPrice,
            }).ToList();
            // var v =  new List<ProductOfferDetails>()
            // {
            //     new Models.ProductOfferDetails
            //     {
            //         Id = Guid.NewGuid().ToString(),
            //         ProductName = "test-product-name",
            //         ProductPrice = 120120,
            //         Currency = "AED"
            //     }
            // };
        }
    }
}