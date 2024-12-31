using DTOs.DTOs;
using Entities.Entities;

namespace Repository.Contracts.Interfaces
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
