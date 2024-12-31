﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Online_Shopping_Central.Entities;
using Online_Shopping_Central.Requests;
using Online_Shopping_Central.Responses;
using Online_Shopping_Central.Repository.Contracts;
using Online_Shopping_Central.Service.Contracts;

namespace Online_Shopping_Central.Controllers
{
    [Route("api/authentication/central")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerRepo _customerRepo;
        private readonly ICustomerService _customerService;
        private readonly IOrderService _orderService;
        private readonly ICredentialRepo _credentialRepo;

        public CustomerController(ICustomerRepo customerRepo, ICustomerService customerService,
            IOrderService orderService, ICredentialRepo credentialRepo
            )
        {
            _customerRepo = customerRepo;
            _customerService = customerService;
            _orderService = orderService;
            _credentialRepo = credentialRepo;
        }

        [HttpPost("signup-google/{credentialId}")]
        public async Task<IActionResult> SignUpGoodle(string credentialId, [FromBody] DistributedCustomer distributedCustomer)
        {
            var newCustomer = await _customerService.CreateNewUser(distributedCustomer);

            Credential credential = new()
            {
                Id = credentialId,
                Provider = "Google",
                CustomerId = newCustomer.Id,
            };

            await _credentialRepo.CreateCredentialAsync(credential);

            return CreatedAtAction("GetProfileUser", new { id = newCustomer.Id }, newCustomer);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] DistributedCustomer distributedCustomer)
        {
            var newCustomer = await _customerService.CreateNewUser(distributedCustomer);

            return CreatedAtAction("GetProfileUser", new { id = newCustomer.Id }, newCustomer);
        }

        [HttpPut("profile")]
        public async Task<IActionResult> UpdateUser([FromBody] DistributedCustomer distributedCustomer)
        {
            var check = await _customerService.UpdateInforUser(distributedCustomer);
            if (!check)
                return BadRequest("Cannot update user");

            return Ok();
        }

        [HttpGet("profile/{Id}")]
        public async Task<IActionResult> GetProfileCustomer(string Id)
        {
            var customer = await _customerService.GetProfileUser(Id);
            if (customer == null)
                return NotFound(new Response<string>
                {
                    Message = "This customer does not exist!"
                });

            return Ok(customer);
        }
    }
}