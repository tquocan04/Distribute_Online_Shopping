using Online_Shopping_Central.Entities;

namespace Online_Shopping_Central.Repository.Contracts
{
    public interface IVoucherRepo
    {
        Task CreateNewVoucherAsync(Voucher voucher);
        Task<bool> CheckCodeVoucher(string code);
        Task<bool> CheckCodeVoucherById(Guid id, string code);
        Task<bool> IsValidVoucherById(Guid? id, DateOnly dateOnly, decimal? minOrderValue, int? quantity);
        Task<IEnumerable<Voucher>> GetVoucherListAsync();
        Task<Voucher?> GetDetailVoucherByCode(string code);
        Task<Voucher> GetDetailVoucherByIdAsync(Guid? id);
        Task UpdateVoucherAsync(Voucher voucher);
        Task UpdateVoucherQuantityAsync(Guid id, int? quantity);
        Task<Guid?> GetVoucherIdByCode(string code);
    }
}
