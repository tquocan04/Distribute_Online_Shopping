using Online_Shopping_Central.DTOs;
using Online_Shopping_Central.Entities;
using Online_Shopping_Central.Requests;

namespace Online_Shopping_Central.Service.Contracts
{
    public interface ICustomerService
    {
        Task<Customer> CreateNewUser(DistributedCustomer distributedCustomer);
        Task DeleteCustomer(Guid id);
        Task<bool> UpdateInforUser(DistributedCustomer distributedCustomer);
        Task<CustomerDTO> GetProfileUser(string userId);
    }
}
