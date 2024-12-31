using Online_Shopping_North.DTOs;
using Online_Shopping_North.Entities;
using Online_Shopping_North.Requests;

namespace Online_Shopping_North.Service.Contracts
{
    public interface ICustomerService
    {
        Task<Customer> CreateNewUser(DistributedCustomer distributedCustomer);
        Task DeleteCustomer(Guid id);
        Task<bool> UpdateInforUser(DistributedCustomer distributedCustomer);
        Task<CustomerDTO> GetProfileUser(string userId);
    }
}
