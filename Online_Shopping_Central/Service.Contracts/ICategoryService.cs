using Online_Shopping_Central.DTOs;
using Online_Shopping_Central.Requests;

namespace Online_Shopping_Central.Service.Contracts
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryDTO>> GetAllCategory();
        Task<CategoryDTO> GetCategoryById(string Id);
        Task DeleteCategoryById(string Id);
        Task UpdateCategoryById(Guid Id, RequestCategory requestCategory);
    }
}
