using Microsoft.EntityFrameworkCore;
using Online_Shopping_Central.Entities;
using Online_Shopping_Central.Repository.Contracts;

namespace Online_Shopping_Central.Repositories
{
    public class CustomerRepo(ApplicationContext applicationContext) : ICustomerRepo
    {
        private readonly ApplicationContext _applicationContext = applicationContext;

        public async Task CreateNewCustomer(Customer customer)
        {
            await _applicationContext.Customers.AddAsync(customer);
            await _applicationContext.SaveChangesAsync();
        }

        public async Task<Customer?> GetCustomerByIdAsync(Guid id)
        {
            return await _applicationContext.Customers.FindAsync(id);
        }

        public async Task UpdateInforCustomer(Customer customer)
        {
            _applicationContext.Customers.Update(customer);
            await _applicationContext.SaveChangesAsync();
        }

        public async Task DeleteCustomerAsync(Customer customer)
        {
            _applicationContext.Customers.Remove(customer);
            await _applicationContext.SaveChangesAsync();
        }

        public async Task<string> GetCustomerNameByIdAsync(Guid id)
        {
            return await _applicationContext.Customers
                .AsNoTracking()
                .Where(ca => ca.Id == id)
                .Select(ca => ca.Name)
                .FirstOrDefaultAsync();
        }
    }
}
