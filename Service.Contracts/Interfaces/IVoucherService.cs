using DTOs.DTOs;
using DTOs.Request;
using Entities.Entities;

namespace Service.Contracts.Interfaces
{
    public interface IVoucherService
    {
        bool CheckExpireTime(int year, int month, int day);
        Task<VoucherDTO> CreateNewVoucher(RequestVoucher requestVoucher);
        Task<IEnumerable<VoucherDTO>> GetAllVouchers();
        Task<VoucherDTO> GetDetailVoucher(Guid id);
        Task<Voucher?> UpdateVoucher(Guid id, RequestVoucher requestVoucher);
    }
}
