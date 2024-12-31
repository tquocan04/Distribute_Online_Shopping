using Online_Shopping_North.Entities;

namespace Online_Shopping_North.Repository.Contracts
{
    public interface ICategoryRepo
    {
        Task CreateNewCategoryAsync(Category category);
        Task<IEnumerable<Category>> GetAllCategoryAsync();
        Task<Category> GetCategoryByIdAsync(Guid Id);
        Task DeleteCategoryByIdAsync(Category category);
        Task UpdateCategoryAsync(Category category);
    }
}
