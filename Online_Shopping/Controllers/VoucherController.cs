using AutoMapper;
using DTOs.DTOs;
using DTOs.Request;
using DTOs.Responses;
using Entities.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository.Contracts.Interfaces;
using Service.Contracts.Interfaces;
using Services.Services;
using System.Net.Http;

namespace Online_Shopping.Controllers
{
    [Route("api/vouchers")]
    [ApiController]
    public class VoucherController : ControllerBase
    {
        private readonly IVoucherService _voucherService;
        private readonly IVoucherRepo _voucherRepo;
        private readonly IMapper _mapper;
        private readonly HttpClient _httpClient;

        private readonly string apiNorth = "http://localhost:5285/api/vouchers/north";
        private readonly string apiSouth = "http://localhost:5125/api/vouchers/south";
        private readonly string apiCentral = "http://localhost:5257/api/vouchers/central";

        public VoucherController(IVoucherService voucherService, IVoucherRepo voucherRepo,
            IMapper mapper, HttpClient httpClient)
        {
            _voucherService = voucherService;
            _voucherRepo = voucherRepo;
            _mapper = mapper;
            _httpClient = httpClient;
        }

        [HttpPost("new-voucher")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateNewVoucher([FromBody] RequestVoucher requestVoucher)
        {
            if (requestVoucher == null)
            {
                return BadRequest(new Response<string>
                {
                    Message = "Invalid information"
                });
            }

            VoucherDTO voucher = await _voucherService.CreateNewVoucher(requestVoucher);

            if(voucher == null)
            {
                return BadRequest(new Response<string>
                {
                    Message = "Cannot create new voucher!!!"
                });
            }

            await _httpClient.PostAsJsonAsync($"{apiNorth}/new-voucher", voucher);
            await _httpClient.PostAsJsonAsync($"{apiSouth}/new-voucher", voucher);
            await _httpClient.PostAsJsonAsync($"{apiCentral}/new-voucher", voucher);

            return CreatedAtAction(nameof(GetDetailVoucher), new { id = voucher.Id }, voucher);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAllVouchers()
        {
            var list = await _voucherService.GetAllVouchers();
            
            if (!list.Any())
            {
                return NotFound(new Response<string>
                {
                    Message = "Does not have any vouchers!"
                });
            }

            return Ok(list);
        }
        [HttpGet("public")]
        public async Task<IActionResult> GetAllVouchersPublic()
        {
            var list = await _voucherService.GetAllVouchers();

            if (!list.Any())
                return NotFound(new Response<string>
                {
                    Message = "Does not have any vouchers!"
                });

            return Ok(list);
        }

        [HttpGet("detail")]
        [Authorize]
        public async Task<IActionResult> GetDetailVoucher([FromQuery] string code)
        {
            Voucher? voucher = await _voucherRepo.GetDetailVoucherByCode(code);

            if (voucher == null)
                return NotFound(new Response<string>
                {
                    Message = "This voucher does not exist!"
                });

            else if (!voucher.checkExpireDate())
                return BadRequest(new Response<string>
                {
                    Message = "This voucher has expired!"
                });

            else if (voucher.Quantity <= 0)
                return BadRequest(new Response<string>
                {
                    Message = "This voucher is out of stock!"
                });

            return Ok(_mapper.Map<VoucherDTO>(voucher));
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateVoucher(Guid id, [FromBody] RequestVoucher requestVoucher)
        {
            if (requestVoucher == null)
                return NotFound(new Response<string>
                {
                    Message = "Invalid new information!"
                });
            

            var existingvoucher = await _voucherService.GetDetailVoucher(id);
            if (existingvoucher == null)
                return NotFound(new Response<string>
                {
                    Message = "This voucher does not exist to update!"
                });

            Voucher? voucher = await _voucherService.UpdateVoucher(id, requestVoucher);
            if (voucher == null)
                return BadRequest(new Response<string>
                {
                    Message = "Expire time or Code voucher are invalid! Please check again!"
                });

            await _httpClient.PutAsJsonAsync($"{apiNorth}/{id}", voucher);
            await _httpClient.PutAsJsonAsync($"{apiSouth}/{id}", voucher);
            await _httpClient.PutAsJsonAsync($"{apiCentral}/{id}", voucher);

            return Ok(requestVoucher);
        }
    }
}
