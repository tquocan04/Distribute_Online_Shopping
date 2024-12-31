using Online_Shopping_South.DTOs;

namespace Online_Shopping_South.Service.Contracts
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDTO>> GetAllProducts();
        Task<ProductDTO> GetProductById(string id);
        Task DeleteProduct(string id);
    }
}
