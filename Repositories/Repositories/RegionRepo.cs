using Entities.Entities;
using Microsoft.EntityFrameworkCore;
using Online_Shopping.Context;
using Repository.Contracts.Interfaces;

namespace Repositories.Repositories
{
    public class RegionRepo : IRegionRepo
    {
        private readonly ApplicationContext _applicationContext;

        public RegionRepo(ApplicationContext applicationContext) 
        {
            _applicationContext = applicationContext;
        }

        public async Task<IEnumerable<Region>> GetAllRegions()
        {
            return await _applicationContext.Regions.AsNoTracking().Include(r => r.Cities).ThenInclude(c => c.Districts).ToListAsync();
        }
    }
}
