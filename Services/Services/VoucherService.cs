using AutoMapper;
using DTOs.DTOs;
using DTOs.Request;
using Entities.Entities;
using Repository.Contracts.Interfaces;
using Service.Contracts.Interfaces;
using System.Runtime.InteropServices;

namespace Services.Services
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

        public bool CheckExpireTime(int year, int month, int day)
        {
            if (year < DateTime.Now.Year)
            {
                return false;
            }

            if (month < DateTime.Now.Month)
            {
                return false;
            }
            
            if (day <= DateTime.Now.Day)
            {
                return false;
            }

            return true;
        }

        public async Task<VoucherDTO> CreateNewVoucher(RequestVoucher requestVoucher)
        {
            if (await _voucherRepo.CheckCodeVoucher(requestVoucher.Code)
                || !CheckExpireTime(requestVoucher.Year, requestVoucher.Month, requestVoucher.Day))
                return null;

            Voucher voucher = new Voucher
            {
                Id = Guid.NewGuid()
            };
            _mapper.Map(requestVoucher, voucher);
            voucher.ExpiryDate = new DateOnly(requestVoucher.Year, requestVoucher.Month, requestVoucher.Day);

            await _voucherRepo.CreateNewVoucherAsync(voucher);

            VoucherDTO voucherDTO = new VoucherDTO();
            _mapper.Map(voucher, voucherDTO);

            return voucherDTO;
        }

        public async Task<IEnumerable<VoucherDTO>> GetAllVouchers()
        {
            var list = await _voucherRepo.GetVoucherListAsync();

            var result = _mapper.Map<IEnumerable<VoucherDTO>>(list);
            return result;
        }

        public async Task<VoucherDTO> GetDetailVoucher(Guid id)
        {
            Voucher voucher = await _voucherRepo.GetDetailVoucherByIdAsync(id);
            return _mapper.Map<VoucherDTO>(voucher);
        }

        public async Task<Voucher?> UpdateVoucher(Guid id, RequestVoucher requestVoucher)
        {
            if (!CheckExpireTime(requestVoucher.Year, requestVoucher.Month, requestVoucher.Day)
                || await _voucherRepo.CheckCodeVoucherById(id, requestVoucher.Code))
                return null;

            Voucher voucher = new Voucher
            {
                Id = id,
            };

            _mapper.Map(requestVoucher, voucher);
            voucher.ExpiryDate = new DateOnly(requestVoucher.Year, requestVoucher.Month, requestVoucher.Day);
            await _voucherRepo.UpdateVoucherAsync(voucher);

            return voucher;
        }
    }
}
