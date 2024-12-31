using Online_Shopping_South.DTOs;
using Online_Shopping_South.Entities;
using Online_Shopping_South.Requests;

namespace Online_Shopping_South.Service.Contracts
{
    public interface ICustomerService
    {
        Task<Customer> CreateNewUser(DistributedCustomer distributedCustomer);
        Task DeleteCustomer(Guid id);
        Task<bool> UpdateInforUser(DistributedCustomer distributedCustomer);
        Task<CustomerDTO> GetProfileUser(string userId);
    }
}
