using Microsoft.EntityFrameworkCore;
using Online_Shopping.Context;
using Repository.Contracts.Interfaces;
using System.Security.Claims;

namespace Repositories.Repositories
{
    public class LoginRepo : ILoginRepo
    {
        private readonly ApplicationContext _applicationContext;

        public LoginRepo(ApplicationContext applicationContext) 
        {
            _applicationContext = applicationContext;
        }

        
        public async Task<string> checkLoginCustomerAsync(string login, string password)
        {
            var customer = await _applicationContext.Customers
                    .AsNoTracking()
                    .FirstOrDefaultAsync(u => u.Email.ToLower() == login.ToLower() && u.Password == password);
            if (customer != null)
            {
                return "Customer";
            }
            return null;
        }

        public async Task<string> checkLoginEmployeeAsync(string login, string password)
        {
            var emp = await _applicationContext.Employees
                    .AsNoTracking()
                    .FirstOrDefaultAsync(e => e.Username.ToLower() == login.ToLower() && e.Password == password);
            if (emp != null)
            {
                return emp.RoleId;
            }
            return null;
        }

        public async Task<Guid> GetCustomerIdFromEmail(string account)
        {
            var id = await _applicationContext.Customers
                    .AsNoTracking()
                    .Where(u => u.Email.ToLower() == account.ToLower())
                    .Select(u => u.Id)
                    .FirstOrDefaultAsync();
            
            return id;
        }

        public async Task<Guid> GetEmployeeIdFromUsername(string account)
        {
            var id = await _applicationContext.Employees
                    .AsNoTracking()
                    .Where(u => u.Username.ToLower() == account.ToLower())
                    .Select(u => u.Id)
                    .FirstOrDefaultAsync();

            return id;
        }
    }
}
