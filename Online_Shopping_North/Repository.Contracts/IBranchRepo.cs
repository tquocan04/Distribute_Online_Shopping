using Online_Shopping_North.Entities;

namespace Online_Shopping_North.Repository.Contracts
{
    public interface IBranchRepo
    {
        Task<Branch> AddNewBranchAsync(Branch branch);
        Task<IEnumerable<Branch>> GetBranchListAsync();
        Task<Branch> GetBranchAsync(Guid id);
        Task DeleteBranchAsync(Branch branch);
        Task UpdateBranchAsync(Branch branch);
    }
}
