using Online_Shopping_Central.DTOs;
using Online_Shopping_Central.Requests;

namespace Online_Shopping_Central.Service.Contracts
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
