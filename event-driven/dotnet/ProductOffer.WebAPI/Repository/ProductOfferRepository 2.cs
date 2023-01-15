using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;
using ProductOffer.WebAPI.Options;

namespace ProductOffer.WebAPI.Repository
{
    public class ProductOfferRepository
    {
        private readonly ProductOfferDbOptions _productOfferDbOptions;
        private readonly IMongoCollection<Data.ProductOfferDetails> _productOffersCollection;
        private readonly MongoClient _mongoClient;
        private readonly IMongoDatabase _mongoDatabase;

        public ProductOfferRepository(ProductOfferDbOptions productOfferDbOptions)
        {
            _productOfferDbOptions =
                productOfferDbOptions ?? throw new ArgumentNullException(nameof(productOfferDbOptions));

            _mongoClient = new MongoClient(
                _productOfferDbOptions.ConnectionString);

            _mongoDatabase = _mongoClient.GetDatabase(
                _productOfferDbOptions.DatabaseName);

            _productOffersCollection = _mongoDatabase.GetCollection<Data.ProductOfferDetails>(
                _productOfferDbOptions.ProductOfferCollectionName);
        }


        public async Task<List<Data.ProductOfferDetails>> GetAsync() =>
            await _productOffersCollection.Find(_ => true).ToListAsync();

        public async Task<Data.ProductOfferDetails?> GetAsync(string productId) =>
            await _productOffersCollection.Find(x => x.ProductId == productId).FirstOrDefaultAsync();
    }
}