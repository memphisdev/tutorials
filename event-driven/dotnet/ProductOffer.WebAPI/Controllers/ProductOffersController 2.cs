using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProductOffer.WebAPI.Services.Commands;
using ProductOffer.WebAPI.Services.Queries;

namespace ProductOffer.WebAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProductOffersController : ControllerBase
    {
        private readonly IMediator _mediator;

        
        public ProductOffersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        
        [HttpGet("list")]
        public async Task<List<Models.ProductOfferDetails>> ProductOffersListAsync()
        {
            return await _mediator.Send(new GetProductOfferDetailsQuery());
        }

        [HttpGet("filter-list")]
        public async Task<Models.ProductOfferDetails> GetProductOffersByProductIdAsync(string productId)
        {
            return await _mediator.Send(new GetProductOfferDetailsByProductIdQuery(productId));
        }

        [HttpPost("send-product-offer")]
        public async Task<ActionResult> SendProductOfferAsync(Models.SendProductOfferDetails productOfferDetails)
        {
            await _mediator.Send(new SendProductOfferDetailsCommand(productOfferDetails));
            return base.Ok();
        }
    }
}