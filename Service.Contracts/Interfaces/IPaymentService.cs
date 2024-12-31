using DTOs.DTOs;

namespace Service.Contracts.Interfaces
{
    public interface IPaymentService
    {
        Task<IEnumerable<PaymentDTO>> GetPaymentListAsync();
    }
}
