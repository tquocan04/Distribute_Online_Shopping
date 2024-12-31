using Entities.Entities;

namespace Repository.Contracts.Interfaces
{
    public interface IEmployeeRepo
    {
        Task AddNewStaff(Employee employee);
        Task DeleteStaffAsync(Employee employee);
        Task<Employee> GetStaffAsync(Guid id);
        Task UpdateProfileStaff(Employee employee);
        Task<bool> CheckUsername(Guid id, string username);
        Task<Employee?> CheckExistingEmployeeByBranchId(Guid id);
        Task<Guid> GetIdByUsername(string username);
    }
}
