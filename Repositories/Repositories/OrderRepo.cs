using DTOs.DTOs;
using Entities.Entities;
using Microsoft.EntityFrameworkCore;
using Online_Shopping.Context;
using Repository.Contracts.Interfaces;
using System;

namespace Repositories.Repositories
{
    public class OrderRepo : IOrderRepo
    {
        private readonly ApplicationContext _applicationContext;

        public OrderRepo(ApplicationContext applicationContext) 
        {
            _applicationContext = applicationContext;
        }

        public async Task AddItemToCart(Item item)
        {
            _applicationContext.Items.Add(item);
            
            await _applicationContext.SaveChangesAsync();
        }

        public async Task CreateOrder(Order order)
        {
            _applicationContext.Orders.Add(order);
            await _applicationContext.SaveChangesAsync();
        }

        public async Task DeleteItemInCart(Item item)
        {
            _applicationContext.Items.Remove(item);
            await _applicationContext.SaveChangesAsync();
        }

        public async Task<Item> GetItem(Guid orderId, Guid prodId)
        {
            var existingItem = await _applicationContext.Items.AsNoTracking()
                .FirstOrDefaultAsync(i => i.OrderId == orderId
                        && i.ProductId == prodId);
            return existingItem;
        }

        public async Task<Order> GetOrderIsCartByCusId(Guid cusId)
        {
            return await _applicationContext.Orders.AsNoTracking().Include(o => o.Items)
                .FirstAsync(o => o.CustomerId == cusId
                                     && o.IsCart == true);
        }

        public async Task<Guid> GetOrderIdByCusId(Guid cusId)
        {
            return await _applicationContext.Orders
                .AsNoTracking()
                .Where(o => o.CustomerId == cusId)
                .Select(o => o.Id)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<ItemDTO>> GetListItemInCart(Guid cartId)
        {
            return await _applicationContext.Items
                .Where(i => i.OrderId == cartId)
                .Select(i => new ItemDTO
                {
                    ProductId = i.ProductId,
                    Name = i.Product.Name,  
                    Quantity = i.Quantity,
                    Price = i.Product.Price,
                })
                .ToListAsync();
        }

        public async Task UpdateQuantityItemToCart(Item item)
        {
            _applicationContext.Items.Update(item);
            await _applicationContext.SaveChangesAsync();
        }

        public async Task UpdateTotalPriceCart(Guid orderId, decimal totalPrice)
        {
            var order = await _applicationContext.Orders.AsNoTracking()
                .FirstAsync(o => o.Id == orderId);
            order.TotalPrice = totalPrice;
            _applicationContext.Orders.Update(order);
            await _applicationContext.SaveChangesAsync();
        }

        public async Task<List<Order>> GetListOrderByCusId(Guid cusId)
        {
            return await _applicationContext.Orders
                .AsNoTracking()
                .Where(o => o.CustomerId == cusId)
                //.Select(o => o.Items)
                //.Include(o => o.Items)
                //    .ThenInclude(i => i.Product)
                .ToListAsync();
        }

        public async Task<List<Item>> GetListItemByOrderId(Guid orderId)
        {
            return await _applicationContext.Items
                .AsNoTracking()
                .Where(o => o.OrderId == orderId)
                //.Select(o => o.Items)
                //.Include(o => o.Items)
                //    .ThenInclude(i => i.Product)
                .ToListAsync();
        }
    }
}
