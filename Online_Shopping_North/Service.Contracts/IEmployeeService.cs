using Online_Shopping_North.DTOs;
using Online_Shopping_North.Requests;

namespace Online_Shopping_North.Service.Contracts
{
    public interface IEmployeeService
    {
        Task AddNewEmployee(Guid Id, RequestEmployee employee);
        Task DeleteEmployee(string id);
        Task<EmployeeDTO> GetProfileEmployee(string id);
        Task UpdateProfile(string id, RequestEmployee requestEmployee);
    }
}
