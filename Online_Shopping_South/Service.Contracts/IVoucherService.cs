using Online_Shopping_South.DTOs;

namespace Online_Shopping_South.Service.Contracts
{
    public interface IVoucherService
    {
        Task CreateNewVoucher(VoucherDTO voucherDTO);
        Task<IEnumerable<VoucherDTO>> GetAllVouchers();
    }
}
