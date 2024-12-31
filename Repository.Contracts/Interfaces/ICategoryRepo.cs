using Entities.Entities;

namespace Repository.Contracts.Interfaces
{
    public interface ICategoryRepo
    {
        Task CreateNewCategoryAsync(Category category);
        Task<IEnumerable<Category>> GetAllCategoryAsync();
        Task<Category> GetCategoryByIdAsync(Guid Id);
        Task DeleteCategoryByIdAsync(Guid categoryId);
        Task<bool> UpdateCategoryAsync(Category category);
        Task<string> GetCategoryNameAsync(Guid categoryId);
    }
}
