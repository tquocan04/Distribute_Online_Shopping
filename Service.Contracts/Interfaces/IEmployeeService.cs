using DTOs.DTOs;
using DTOs.Request;

namespace Service.Contracts.Interfaces
{
    public interface IEmployeeService
    {
        Task<EmployeeDTO> AddNewEmployee(RequestEmployee employee);
        Task DeleteEmployee(Guid id);
        Task<EmployeeDTO> GetProfileEmployee(Guid id);
        Task<bool> UpdateProfile(Guid id, RequestEmployee requestEmployee);
    }
}
