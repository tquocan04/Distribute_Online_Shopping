using DTOs.DTOs;

namespace Service.Contracts.Interfaces
{
    public interface IShippingService
    {
        Task<IEnumerable<ShippingDTO>> GetShippingListAsync();
    }
}
