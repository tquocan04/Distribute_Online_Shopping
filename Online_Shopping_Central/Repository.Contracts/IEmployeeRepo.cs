using Online_Shopping_Central.Entities;

namespace Online_Shopping_Central.Repository.Contracts
{
    public interface IEmployeeRepo
    {
        Task AddNewStaff(Employee employee);
        Task DeleteStaffAsync(Employee employee);
        Task<Employee> GetStaffAsync(Guid id);
        Task UpdateProfileStaff(Employee employee);
    }
}
