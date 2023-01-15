using System;
using MongoDB.Bson.Serialization.Attributes;

namespace ProductOffer.Processor.Data
{
    public class ProductOfferDetails
    {
        
        [BsonId]
        public string Id { get; set; }
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal ProductPrice { get; set; }
        public string Currency { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}