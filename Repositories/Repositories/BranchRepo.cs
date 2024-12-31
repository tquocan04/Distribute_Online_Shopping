using Entities.Entities;
using Microsoft.EntityFrameworkCore;
using Online_Shopping.Context;
using Repository.Contracts.Interfaces;

namespace Repositories.Repositories
{
    public class BranchRepo : IBranchRepo
    {
        private readonly ApplicationContext _applicationContext;
        private readonly ICityRepo _cityRepo;

        public BranchRepo(ApplicationContext applicationContext, ICityRepo cityRepo) 
        {
            _applicationContext = applicationContext;
            _cityRepo = cityRepo;
        }

        public async Task<Branch> AddNewBranchAsync(Branch branch)
        {
            await _applicationContext.Branches.AddAsync(branch);
            await _applicationContext.SaveChangesAsync();
            _applicationContext.Entry(branch).State = EntityState.Detached;
            return branch;
        }

        public async Task DeleteBranchAsync(Branch branch)
        {
            _applicationContext.Branches.Remove(branch);
            await _applicationContext.SaveChangesAsync();
        }

        public async Task<Branch> GetBranchAsync(Guid id)
        {
            return await _applicationContext.Branches.AsNoTracking()
                .FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task<IEnumerable<Branch>> GetBranchListAsync()
        {
            return await _applicationContext.Branches.ToListAsync();
        }

        public async Task UpdateBranchAsync(Branch branch)
        {
            _applicationContext.Branches.Update(branch);
            await _applicationContext.SaveChangesAsync();
        }
    }
}
