using Online_Shopping_North.Entities;

namespace Online_Shopping_North.Repository.Contracts
{
    public interface IEmployeeRepo
    {
        Task AddNewStaff(Employee employee);
        Task DeleteStaffAsync(Employee employee);
        Task<Employee> GetStaffAsync(Guid id);
        Task UpdateProfileStaff(Employee employee);
    }
}
