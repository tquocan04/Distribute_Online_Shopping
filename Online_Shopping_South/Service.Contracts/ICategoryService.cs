using Online_Shopping_South.DTOs;
using Online_Shopping_South.Requests;

namespace Online_Shopping_South.Service.Contracts
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryDTO>> GetAllCategory();
        Task<CategoryDTO> GetCategoryById(string Id);
        Task DeleteCategoryById(string Id);
        Task UpdateCategoryById(Guid Id, RequestCategory requestCategory);
    }
}
