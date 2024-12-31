using Entities.Entities;

namespace Repository.Contracts.Interfaces
{
    public interface IPaymentRepo
    {
        Task<IEnumerable<Payment?>> GetAllPaymentsAsync();
        Task<string?> GetPaymentIdAsync(string? id);
    }
}
