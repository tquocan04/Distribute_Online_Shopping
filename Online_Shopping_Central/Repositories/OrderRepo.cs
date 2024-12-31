using Microsoft.EntityFrameworkCore;
using Online_Shopping_Central.Entities;
using Online_Shopping_Central.Repository.Contracts;

namespace Online_Shopping_Central.Repositories
{
    public class OrderRepo(ApplicationContext applicationContext) : IOrderRepo
    {
        private readonly ApplicationContext _applicationContext = applicationContext;

        public async Task AddItemToCart(Item item)
        {
            await _applicationContext.Items.AddAsync(item);

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

        public async Task DeleteOrderAsync(Guid id)
        {
            var order = await _applicationContext.Orders
                .AsNoTracking()
               .FirstOrDefaultAsync(o => o.Id == id);

            _applicationContext.Orders.Remove(order);
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
    }
}
