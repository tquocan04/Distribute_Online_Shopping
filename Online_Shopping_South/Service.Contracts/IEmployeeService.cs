using Online_Shopping_South.DTOs;
using Online_Shopping_South.Requests;

namespace Online_Shopping_South.Service.Contracts
{
    public interface IEmployeeService
    {
        Task AddNewEmployee(Guid Id, RequestEmployee employee);
        Task DeleteEmployee(string id);
        Task<EmployeeDTO> GetProfileEmployee(string id);
        Task UpdateProfile(string id, RequestEmployee requestEmployee);
    }
}
