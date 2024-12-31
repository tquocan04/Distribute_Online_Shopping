using DTOs.MongoDb;
using MongoDB.Driver;
using Service.Contracts;
using Services.MongoDB;
using System.Reflection.Metadata;

namespace Services
{
    public class MetadataService : IMetadataService
    {
        private readonly MongoDBClient _mongoDbClient;
        private readonly IMongoCollection<CustomerMetadata> _customerCollection;
        private readonly IMongoCollection<ProductMetadata> _productMetadata;

        public MetadataService(MongoDBClient mongoDbClient)
        {
            _mongoDbClient = mongoDbClient;
            _customerCollection = _mongoDbClient.GetCollection<CustomerMetadata>("customer");
            _productMetadata = _mongoDbClient.GetCollection<ProductMetadata>("product");
        }
        public async Task CreateCustomerMetadataAsync(CustomerMetadata metadata)
        {
            await _customerCollection.InsertOneAsync(metadata);
        }

        public async Task UpdateCustomerMetadataAsync(CustomerMetadata metadata)
        {
            var filter = Builders<CustomerMetadata>.Filter.Eq(c => c.Id, metadata.Id);
            var update = Builders<CustomerMetadata>.Update
                .Set(c => c.Name, metadata.Name)
                .Set(c => c.Email, metadata.Email)
                .Set(c => c.Password, metadata.Password)
                .Set(c => c.PhoneNumber, metadata.PhoneNumber)
                .Set(c => c.Picture, metadata.Picture)
                .Set(c => c.Dob, metadata.Dob)
                .Set(c => c.Gender, metadata.Gender)
                .Set(c => c.RegionName, metadata.RegionName)
                .Set(c => c.CityName, metadata.CityName)
                .Set(c => c.DistrictName, metadata.DistrictName)
                .Set(c => c.Street, metadata.Street)
                ;

            await _customerCollection.UpdateOneAsync(filter, update);
        }

        //-------------------------------------PRODUCT-----------------------------------------------------
        public async Task CreateProductMetadataAsync(ProductMetadata metadata)
        {
            await _productMetadata.InsertOneAsync(metadata);
        }

        public async Task<ProductMetadata> GetProductByIdAsync(string id)
        {
            return await _productMetadata.Find(p => p.Id == id).FirstOrDefaultAsync();
        }

        public async Task UpdateProductMetadataAsync(ProductMetadata metadata)
        {
            var filter = Builders<ProductMetadata>.Filter.Eq(p => p.Id, metadata.Id);
            var update = Builders<ProductMetadata>.Update
                .Set(p => p.Name, metadata.Name)
                .Set(p => p.Description, metadata.Description)
                .Set(p => p.Price, metadata.Price)
                .Set(p => p.Image, metadata.Image)
                .Set(p => p.CategoryName, metadata.CategoryName)
                ;

            await _productMetadata.UpdateOneAsync(filter, update);
        }

        public async Task UpdateProductActionAsync(string id)
        {
            ProductMetadata product = await _productMetadata.Find(p => p.Id == id).FirstOrDefaultAsync();
            if (product == null)
            {
                return;
            }
            
            product.IncreaseAction(1);
            product.IncreaseTotal(1);
            await _productMetadata.ReplaceOneAsync(p => p.Id == id, product);
        }

        public async Task UpdateProductPurchaseAsync(string id, long quantity)
        {
            ProductMetadata product = await _productMetadata.Find(p => p.Id == id).FirstOrDefaultAsync();
            if (product == null)
            {
                return;
            }
            product.IncreasePurchase(quantity);
            product.IncreaseTotal(quantity);
            await _productMetadata.ReplaceOneAsync(p => p.Id == id, product);
        }

        public async Task DeleteProductMetadataAsync(ProductMetadata metadata)
        {
            var filter = Builders<ProductMetadata>.Filter.Eq(p => p.Id, metadata.Id);
            await _productMetadata.DeleteOneAsync(filter);
        }

        public async Task<List<string>> GetProductsByTotalActionsAsync()
        {
            var sortDefinition = Builders<ProductMetadata>.Sort.Descending(p => p.Total);
            var products = await _productMetadata
                .Find(FilterDefinition<ProductMetadata>.Empty)
                .Sort(sortDefinition)
                .ToListAsync();

            return products.Select(p => p.Id).ToList();
        }
    }
}
