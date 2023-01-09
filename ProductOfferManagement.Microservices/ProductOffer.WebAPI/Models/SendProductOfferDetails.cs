using System;

namespace ProductOffer.WebAPI.Models
{
    public class SendProductOfferDetails
    {
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public string Currency { get; set; }
        public decimal ProductPrice { get; set; }
    }
}