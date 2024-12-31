using Entities.Entities;
using Microsoft.EntityFrameworkCore;
using Online_Shopping.Context;
using Repository.Contracts.Interfaces;

namespace Repositories.Repositories
{
    public class BillRepo : IBillRepo
    {
        private readonly ApplicationContext _applicationContext;

        public BillRepo(ApplicationContext applicationContext) 
        {
            _applicationContext = applicationContext;
        }


        public async Task CartToBillAsync(Order order)
        {
            _applicationContext.Orders.Update(order);
            await _applicationContext.SaveChangesAsync();
        }

        public async Task CompletedBillAsync(Guid id)
        {
            var bill = await _applicationContext.Orders.FindAsync(id);
            bill.Status = "Completed";
            await _applicationContext.SaveChangesAsync();
        }

        public async Task<List<Order>> GetListBillForCustomerAsync(Guid customerId)
        {
            return await _applicationContext.Orders
                .AsNoTracking()
                .Include(o => o.Items)
                .Where(o => !o.IsCart && o.CustomerId == customerId)
                .OrderByDescending(o => o.OrderDate)
                .ToListAsync();
        }

        public async Task<Guid?> GetCustomerIdOfBillAsync(Guid orderId)
        {
            return await _applicationContext.Orders
                .AsNoTracking()
                .Where(o => !o.IsCart && o.Id == orderId)
                .Select(o => o.CustomerId)
                .FirstOrDefaultAsync();
        }

        public async Task<Order?> GetBillDetailForEmployeeAsync(Guid orderId)
        {
            return await _applicationContext.Orders
                .AsNoTracking()
                .Include(o => o.Items)
                .Where(o => !o.IsCart && o.Id == orderId)
                .OrderByDescending(o => o.OrderDate)
                .FirstOrDefaultAsync();
        }

        public async Task<List<Order>> GetListCompletedBillAsync()
        {
            return await _applicationContext.Orders
                .AsNoTracking()
                .Include(o => o.Items)
                .Where(o => !o.IsCart && o.Status == "Completed")
                .OrderByDescending(o => o.OrderDate)
                .ToListAsync();
        }

        public async Task<List<Order>> GetListPendingBillAsync()
        {
            return await _applicationContext.Orders
                .AsNoTracking()
                .Include(o => o.Items)
                .Where(o => !o.IsCart && o.Status == "Pending")
                .OrderBy(o => o.OrderDate)
                .ToListAsync();
        }

        public async Task<List<Order>> GetCompletedBillInYearMonthAsync(int year, int month)
        {
            return await _applicationContext.Orders
                .AsNoTracking()
                .Include(o => o.Items)
                .Where(o => o.Status == "Completed" && o.OrderDate.Year == year && o.OrderDate.Month == month)
                .ToListAsync();
        }

        public async Task<List<Order>> GetCompletedBillInYearAsync(int year)
        {
            return await _applicationContext.Orders
                .AsNoTracking()
                .Include(o => o.Items)
                .Where(o => o.Status == "Completed" && o.OrderDate.Year == year)
                .ToListAsync();
        }

        public async Task<List<Order>> GetCompletedBillByCategoryInYearMonthAsync(Guid categoryId, int year, int month)
        {
            return await _applicationContext.Orders
                .AsNoTracking()
                .Include(o => o.Items)
                .Where(o => o.Status == "Completed" && o.OrderDate.Year == year && o.OrderDate.Month == month)
                .ToListAsync();
        }

        public async Task<List<Order>> GetCompletedBillByCategoryInYearAsync(Guid categoryId, int year)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Order>> CustomerGetListPendingBillAsync(Guid customerId)
        {
            return await _applicationContext.Orders
                .AsNoTracking()
                .Include(o => o.Items)
                .Where(o => !o.IsCart && o.Status == "Pending" && o.CustomerId == customerId)
                .OrderBy(o => o.OrderDate)
                .ToListAsync();
        }

        public async Task<List<Order>> CustomerGetListCompletedBillAsync(Guid customerId)
        {
            return await _applicationContext.Orders
                .AsNoTracking()
                .Include(o => o.Items)
                .Where(o => !o.IsCart && o.Status == "Completed" && o.CustomerId == customerId)
                .OrderByDescending(o => o.OrderDate)
                .ToListAsync();
        }
    }
}
