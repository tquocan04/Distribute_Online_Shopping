using DTOs.DTOs;
using DTOs.Request;
using Entities.Entities;

namespace Service.Contracts.Interfaces
{
    public interface IOrderService
    {
        Task<Order> CreateNewCart(Guid cusId);
        Task<OrderCartDTO> GetOrderCart(Guid cusId);
        Task<OrderCartDTO> MergeCartFromClient(Guid cusId, List<RequestItems> items);
        Task AddToCart(Guid cusId, Guid prodId);
        Task DeleteItemInCart(Guid cusId, Guid prodId);
        Task<bool> UpdateQuantityItem(Guid cusId, Guid prodId, int Quantity);
    }
}
