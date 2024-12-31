using Online_Shopping_North.DTOs;
using Online_Shopping_North.Requests;

namespace Online_Shopping_North.Service.Contracts
{
    public interface IVoucherService
    {
        Task CreateNewVoucher(VoucherDTO voucherDTO);
        Task<IEnumerable<VoucherDTO>> GetAllVouchers();
    }
}
