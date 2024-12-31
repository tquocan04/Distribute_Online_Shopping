using Entities.Entities;

namespace Repository.Contracts.Interfaces
{
    public interface IUserRepo
    {
        Task<bool> checkEmailExist(string email);
        Task<bool> checkEmailById(Guid id, string email);
        public bool checkDOB(int year);
        Task CreateNewCustomer(Customer customer);
        Task<Customer?> GetCustomerByIdAsync(Guid id);
        Task<Customer?> GetProfileByCustomerIdIdAsync(Guid id);
        Task<string> GetEmailByCustomerIdAsync(Guid id);
        Task UpdateInforCustomer(Customer customer);
        Task<string?> GetStreetDefaultByCustomerIdAsync(Guid customerId);
        Task<Guid> GetDistrictDefaultByCustomerIdAsync(Guid customerId);
        Task<string?> GetPictureOfCustomer(string email);
        Task<string> GetCustomerNameByIdAsync(Guid id);
    }
}
