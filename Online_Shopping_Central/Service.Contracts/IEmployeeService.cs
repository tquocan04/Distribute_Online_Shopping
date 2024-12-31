using Online_Shopping_Central.DTOs;
using Online_Shopping_Central.Requests;

namespace Online_Shopping_Central.Service.Contracts
{
    public interface IEmployeeService
    {
        Task AddNewEmployee(Guid Id, RequestEmployee employee);
        Task DeleteEmployee(string id);
        Task<EmployeeDTO> GetProfileEmployee(string id);
        Task UpdateProfile(string id, RequestEmployee requestEmployee);
    }
}
