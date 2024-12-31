using Online_Shopping_Central.DTOs;
using Online_Shopping_Central.Requests;

namespace Online_Shopping_Central.Service.Contracts
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
