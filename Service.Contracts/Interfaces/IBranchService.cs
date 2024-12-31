using DTOs.DTOs;
using DTOs.Request;
using Entities.Entities;

namespace Service.Contracts.Interfaces
{
    public interface IBranchService
    {
        Task<BranchDTO> AddNewBranch(RequestBranch requestBranch);
        Task<List<BranchDTO>> GetBranchList();
        Task<BranchDTO> GetBranch(string id);
        Task DeleteBranch(string id);
        Task UpdateBranch(string id, RequestBranch requestBranch);
    }
}
