namespace ProductOffer.WebAPI.Models
{
    public class ProductOfferDetails
    {
        public string Id { get; set; }
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public string Currency { get; set; }
        public decimal ProductPrice { get; set; }
    }
}