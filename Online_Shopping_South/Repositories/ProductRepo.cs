using Microsoft.EntityFrameworkCore;
using Online_Shopping_South.Entities;
using Online_Shopping_South.Repository.Contracts;

namespace Online_Shopping_South.Repositories
{
    public class ProductRepo(ApplicationContext applicationContext) : IProductRepo
    {
        private readonly ApplicationContext _applicationContext = applicationContext;

        public async Task CreateNewProductAsync(Product product)
        {
            _applicationContext.Products.Add(product);
            await _applicationContext.SaveChangesAsync();
        }

        public async Task<Product> GetProductByIdAsync(Guid id)
        {
            return await _applicationContext.Products.AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            return await _applicationContext.Products.ToListAsync();
        }

        public async Task UpdatestatusProduct(Guid id)
        {
            var product = await _applicationContext.Products.FindAsync(id);
            if (product.IsHidden)
                product.IsHidden = false;
            else
                product.IsHidden = true;

            await _applicationContext.SaveChangesAsync();
        }

        public async Task UpdateInforProduct(Product product)
        {
            _applicationContext.Products.Update(product);
            await _applicationContext.SaveChangesAsync();

        }

        public async Task DeleteProductAsync(Product product)
        {
            _applicationContext.Products.Remove(product);
            await _applicationContext.SaveChangesAsync();
        }
    }
}
