using Online_Shopping_Central.DTOs;

namespace Online_Shopping_Central.Service.Contracts
{
    public interface IVoucherService
    {
        Task CreateNewVoucher(VoucherDTO voucherDTO);
        Task<IEnumerable<VoucherDTO>> GetAllVouchers();
    }
}
