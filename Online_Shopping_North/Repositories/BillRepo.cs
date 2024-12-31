﻿using Microsoft.EntityFrameworkCore;
using Online_Shopping_North.Entities;
using Online_Shopping_North.Repository.Contracts;

namespace Online_Shopping_North.Repositories
{
    public class BillRepo(ApplicationContext applicationContext) : IBillRepo
    {
        private readonly ApplicationContext _applicationContext = applicationContext;

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