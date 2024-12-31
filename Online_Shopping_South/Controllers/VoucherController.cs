﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Online_Shopping_South.DTOs;
using Online_Shopping_South.Entities;
using Online_Shopping_South.Repository.Contracts;
using Online_Shopping_South.Service.Contracts;

namespace Online_Shopping_South.Controllers
{
    [Route("api/vouchers/south")]
    [ApiController]
    public class VoucherController : ControllerBase
    {
        private readonly IVoucherService _voucherService;
        private readonly IVoucherRepo _voucherRepo;

        public VoucherController(IVoucherService voucherService, IVoucherRepo voucherRepo)
        {
            _voucherService = voucherService;
            _voucherRepo = voucherRepo;
        }

        [HttpPost("new-voucher")]
        public async Task<IActionResult> CreateNewVoucher([FromBody] VoucherDTO voucherDTO)
        {
            await _voucherService.CreateNewVoucher(voucherDTO);

            return CreatedAtAction(nameof(GetAllVouchers), new { id = voucherDTO.Id });
        }

        [HttpGet]
        public async Task<IEnumerable<VoucherDTO>> GetAllVouchers()
        {
            var list = await _voucherService.GetAllVouchers();

            return list;
        }

        [HttpPut("{id}")]
        public async Task UpdateVoucher(Guid id, [FromBody] Voucher voucher)
        {
            await _voucherRepo.UpdateVoucherAsync(voucher);
        }
    }
}