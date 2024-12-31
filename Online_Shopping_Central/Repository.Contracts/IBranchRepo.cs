using Online_Shopping_Central.Entities;

namespace Online_Shopping_Central.Repository.Contracts
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
