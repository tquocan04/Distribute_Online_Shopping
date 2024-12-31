using Online_Shopping_South.Entities;

namespace Online_Shopping_South.Repository.Contracts
{
    public interface IOrderRepo
    {
        Task CreateOrder(Order order);
        Task UpdateTotalPriceCart(Guid orderId, decimal totalPrice);
        Task<Order> GetOrderIsCartByCusId(Guid cusId);
        Task AddItemToCart(Item item);
        Task UpdateQuantityItemToCart(Item item);
        Task DeleteItemInCart(Item item);
        Task DeleteOrderAsync(Guid id);
        Task<Item> GetItem(Guid orderId, Guid prodId);
    }
}
