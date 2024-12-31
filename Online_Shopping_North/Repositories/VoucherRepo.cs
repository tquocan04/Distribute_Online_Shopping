using Microsoft.EntityFrameworkCore;
using Online_Shopping_North.Entities;
using Online_Shopping_North.Repository.Contracts;

namespace Online_Shopping_North.Repositories
{
    public class VoucherRepo(ApplicationContext applicationContext) : IVoucherRepo
    {
        private readonly ApplicationContext _applicationContext = applicationContext;

        public async Task<bool> CheckCodeVoucher(string code)
        {
            return await _applicationContext.Vouchers
                .AnyAsync(v => v.Code.ToUpper() == code.ToUpper());
        }

        public async Task<bool> CheckCodeVoucherById(Guid id, string code)
        {
            return await _applicationContext.Vouchers
                .AnyAsync(v => v.Code.ToUpper() == code.ToUpper()
                                && v.Id != id);
        }

        public async Task CreateNewVoucherAsync(Voucher voucher)
        {
            _applicationContext.Vouchers.Add(voucher);
            await _applicationContext.SaveChangesAsync();
        }

        public async Task<Voucher> GetDetailVoucherByIdAsync(Guid? id)
        {
            return await _applicationContext.Vouchers.AsNoTracking()
                .FirstOrDefaultAsync(v => v.Id == id);
        }

        public async Task<Guid?> GetVoucherIdByCode(string code)
        {
            return await _applicationContext.Vouchers
                .AsNoTracking()
                .Where(v => v.Code.ToUpper() == code.ToUpper())
                .Select(v => v.Id)
                .FirstOrDefaultAsync();
        }

        public async Task<Voucher?> GetDetailVoucherByCode(string code)
        {
            return await _applicationContext.Vouchers
                .AsNoTracking()
                .FirstOrDefaultAsync(v => v.Code.ToLower() == code.ToLower());
        }

        public async Task<IEnumerable<Voucher>> GetVoucherListAsync()
        {
            return await _applicationContext.Vouchers
                .AsNoTracking()
                .Where(v => v.ExpiryDate >= DateOnly.FromDateTime(DateTime.Now)
                        && v.Quantity > 0)
                .ToListAsync();
        }

        public async Task<bool> IsValidVoucherById(Guid? id, DateOnly dateOnly, decimal? minOrderValue, int? quantity)
        {
            var voucher = await _applicationContext.Vouchers
                .AsNoTracking()
                .Where(v => v.Id == id
                        && v.ExpiryDate > dateOnly
                        && v.MinOrderValue <= minOrderValue
                        && v.Quantity > 0)
                .FirstOrDefaultAsync();

            return true ? voucher != null : false;
        }

        public async Task UpdateVoucherAsync(Voucher voucher)
        {
            _applicationContext.Vouchers.Update(voucher);
            await _applicationContext.SaveChangesAsync();
        }

        public async Task UpdateVoucherQuantityAsync(Guid id, int? quantity)
        {
            Voucher? voucher = await _applicationContext.Vouchers.FindAsync(id);

            voucher.Quantity = quantity;
            await _applicationContext.SaveChangesAsync();
        }
    }
}
