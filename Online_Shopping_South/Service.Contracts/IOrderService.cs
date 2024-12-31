using Online_Shopping_South.DTOs;
using Online_Shopping_South.Requests;

namespace Online_Shopping_South.Service.Contracts
{
    public interface IOrderService
    {
        Task CreateNewCart(Guid orderId, Guid cusId);
        Task<OrderCartDTO> GetOrderCart(Guid cusId);
        Task AddToCart(Guid cusId, Guid prodId);
        Task DeleteItemInCart(Guid cusId, Guid prodId);
        Task<OrderCartDTO> MergeCartFromClient(Guid cusId, List<RequestItems> items);
        Task<bool> UpdateQuantityItem(Guid cusId, Guid prodId, int Quantity);
    }
}
