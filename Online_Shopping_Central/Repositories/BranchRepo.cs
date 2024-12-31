using Microsoft.EntityFrameworkCore;
using Online_Shopping_Central.Entities;
using Online_Shopping_Central.Repository.Contracts;

namespace Online_Shopping_Central.Repositories
{
    public class BranchRepo(ApplicationContext applicationContext) : IBranchRepo
    {
        private readonly ApplicationContext _applicationContext = applicationContext;

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
