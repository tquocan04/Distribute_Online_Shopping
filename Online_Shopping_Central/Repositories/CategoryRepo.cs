using Microsoft.EntityFrameworkCore;
using Online_Shopping_Central.Entities;
using Online_Shopping_Central.Repository.Contracts;

namespace Online_Shopping_Central.Repositories
{
    public class CategoryRepo : ICategoryRepo
    {
        private readonly ApplicationContext _applicationContext;

        public CategoryRepo(ApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
        }

        public async Task CreateNewCategoryAsync(Category category)
        {
            _applicationContext.Categories.Add(category);
            await _applicationContext.SaveChangesAsync();
        }
        public async Task<Category> GetCategoryByIdAsync(Guid Id)
        {
            return await _applicationContext.Categories
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == Id);
        }

        public async Task DeleteCategoryByIdAsync(Category cate)
        {
            _applicationContext.Categories.Remove(cate);
            await _applicationContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Category>> GetAllCategoryAsync()
        {
            return await _applicationContext.Categories.AsNoTracking().ToListAsync();
        }

        public async Task UpdateCategoryAsync(Category category)
        {
            _applicationContext.Categories.Update(category);
            await _applicationContext.SaveChangesAsync();
        }
    }
}
