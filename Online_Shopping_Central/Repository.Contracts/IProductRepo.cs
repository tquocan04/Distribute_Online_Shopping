using Online_Shopping_Central.Entities;

namespace Online_Shopping_Central.Repository.Contracts
{
    public interface IProductRepo
    {
        Task CreateNewProductAsync(Product product);
        Task<IEnumerable<Product>> GetAllProductsAsync();
        Task<Product> GetProductByIdAsync(Guid id);
        Task UpdatestatusProduct(Guid id);
        Task UpdateInforProduct(Product product);
        Task DeleteProductAsync(Product product);
    }
}
