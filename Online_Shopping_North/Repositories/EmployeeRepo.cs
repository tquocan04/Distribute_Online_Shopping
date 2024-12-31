using Microsoft.EntityFrameworkCore;
using Online_Shopping_North.Entities;
using Online_Shopping_North.Repository.Contracts;

namespace Online_Shopping_North.Repositories
{
    public class EmployeeRepo(ApplicationContext applicationContext) : IEmployeeRepo
    {
        private readonly ApplicationContext _applicationContext = applicationContext;

        public async Task AddNewStaff(Employee employee)
        {
            _applicationContext.Employees.Add(employee);
            await _applicationContext.SaveChangesAsync();
        }

        public async Task DeleteStaffAsync(Employee employee)
        {
            _applicationContext.Employees.Remove(employee);
            await _applicationContext.SaveChangesAsync();
        }

        public async Task<Employee> GetStaffAsync(Guid id)
        {
            return await _applicationContext.Employees.AsNoTracking()
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task UpdateProfileStaff(Employee employee)
        {
            _applicationContext.Employees.Update(employee);
            await _applicationContext.SaveChangesAsync();
        }
    }
}
