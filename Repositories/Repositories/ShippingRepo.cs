using Entities.Entities;
using Microsoft.EntityFrameworkCore;
using Online_Shopping.Context;
using Repository.Contracts.Interfaces;

namespace Repositories.Repositories
{
    public class ShippingRepo : IShippingRepo
    {
        private readonly ApplicationContext _applicationContext;

        public ShippingRepo(ApplicationContext applicationContext) 
        {
            _applicationContext = applicationContext;
        }

        public async Task<IEnumerable<ShippingMethod>> GetAllShippingMethods()
        {
            return await _applicationContext.ShippingMethods.ToListAsync();
        }

        public async Task<string?> GetShippingMethodByIdAsync(string? id)
        {
            return await _applicationContext.ShippingMethods
                .AsNoTracking()
                .Where(x => x.Id == id)
                .Select(x => x.Id)
                .FirstOrDefaultAsync();
        }
    }
}
