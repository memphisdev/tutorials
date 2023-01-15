namespace ProductOffer.WebAPI.Options
{
    public class ProductOfferDbOptions
    {
        public string ConnectionString { get; set; } = null!;

        public string DatabaseName { get; set; } = null!;

        public string ProductOfferCollectionName { get; set; } = null!;
    }
}