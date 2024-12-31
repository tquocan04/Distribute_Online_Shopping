using Entities.Entities;
using Microsoft.EntityFrameworkCore;
using Online_Shopping.Context;
using Repository.Contracts.Interfaces;

namespace Repositories.Repositories
{
    public class PaymentRepo : IPaymentRepo
    {
        private readonly ApplicationContext _applicationContext;

        public PaymentRepo(ApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
        }
        public async Task<IEnumerable<Payment?>> GetAllPaymentsAsync()
        {
            return await _applicationContext.Payments.ToListAsync();
        }

        public async Task<string?> GetPaymentIdAsync(string? id)
        {
            return await _applicationContext.Payments
                .AsNoTracking()
                .Where(x => x.Id == id)
                .Select(x => x.Id)
                .FirstOrDefaultAsync();
        }
    }
}
