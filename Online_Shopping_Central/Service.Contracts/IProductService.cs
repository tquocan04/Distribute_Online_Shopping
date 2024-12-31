using Online_Shopping_Central.DTOs;

namespace Online_Shopping_Central.Service.Contracts
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDTO>> GetAllProducts();
        Task<ProductDTO> GetProductById(string id);
        Task DeleteProduct(string id);
    }
}
