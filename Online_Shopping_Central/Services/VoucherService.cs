using AutoMapper;
using Online_Shopping_Central.DTOs;
using Online_Shopping_Central.Entities;
using Online_Shopping_Central.Repository.Contracts;
using Online_Shopping_Central.Service.Contracts;

namespace Online_Shopping_Central.Services
{
    public class VoucherService : IVoucherService
    {
        private readonly IVoucherRepo _voucherRepo;
        private readonly IMapper _mapper;

        public VoucherService(IVoucherRepo voucherRepo, IMapper mapper)
        {
            _voucherRepo = voucherRepo;
            _mapper = mapper;
        }
        public async Task CreateNewVoucher(VoucherDTO voucherDTO)
        {
            Voucher voucher = new();
            _mapper.Map(voucherDTO, voucher);

            await _voucherRepo.CreateNewVoucherAsync(voucher);
        }

        public async Task<IEnumerable<VoucherDTO>> GetAllVouchers()
        {
            var list = await _voucherRepo.GetVoucherListAsync();

            var result = _mapper.Map<IEnumerable<VoucherDTO>>(list);
            return result;
        }
    }
}
