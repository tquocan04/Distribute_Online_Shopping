using Online_Shopping_North.DTOs;
using Online_Shopping_North.Requests;

namespace Online_Shopping_North.Service.Contracts
{
    public interface IBranchService
    {
        Task<BranchDTO> AddNewBranch(string id, RequestBranch requestBranch);
        Task<List<BranchDTO>> GetBranchList();
        Task<BranchDTO> GetBranch(string id);
        Task DeleteBranch(string id);
        Task UpdateBranch(string id, RequestBranch requestBranch);
    }
}
