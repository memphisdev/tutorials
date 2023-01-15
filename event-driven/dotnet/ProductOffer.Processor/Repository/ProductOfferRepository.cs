using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;
using ProductOffer.Processor.Options;

namespace ProductOffer.Processor.Repository
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


        public async Task CreateAsync(Data.ProductOfferDetails offerDetail) =>
            await _productOffersCollection.InsertOneAsync(offerDetail);

        public async Task CreateManyAsync(List<Data.ProductOfferDetails> offerDetails) =>
            await _productOffersCollection.InsertManyAsync(offerDetails);
    }
}