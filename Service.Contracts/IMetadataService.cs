using DTOs.MongoDb;

namespace Service.Contracts
{
    public interface IMetadataService
    {
        //Customer
        Task CreateCustomerMetadataAsync(CustomerMetadata metadata);
        Task UpdateCustomerMetadataAsync(CustomerMetadata metadata);


        //Product
        Task CreateProductMetadataAsync(ProductMetadata metadata);
        Task UpdateProductMetadataAsync(ProductMetadata metadata);
        Task DeleteProductMetadataAsync(ProductMetadata metadata);
        Task<ProductMetadata> GetProductByIdAsync(string id);
        Task<List<string>> GetProductsByTotalActionsAsync();
        Task UpdateProductActionAsync(string id);
        Task UpdateProductPurchaseAsync(string id, long quantity);
    }
}
