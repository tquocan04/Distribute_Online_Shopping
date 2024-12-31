using AutoMapper;
using DTOs;
using DTOs.Request;
using DTOs.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repositories.Repositories;
using Repository.Contracts.Interfaces;
using Service.Contracts.Interfaces;
using Services.Services;

namespace Online_Shopping.Controllers
{
    [Route("api/authentication")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILoginService _loginService;
        private readonly ITokenService _tokenService;
        private readonly IUserRepo _userRepo;
        private readonly ICredentialRepo _credentialRepo;

        public LoginController(ILoginService loginService, ITokenService tokenService,
            IUserRepo userRepo, ICredentialRepo credentialRepo) 
        {
            _loginService = loginService;
            _tokenService = tokenService;
            _userRepo = userRepo;
            _credentialRepo = credentialRepo;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] RequestLogin requestLogin)
        {
            if(!ModelState.IsValid)
                return BadRequest("Please fill in all login information");

            var result = await _loginService.LoginAsync(requestLogin);
            string role = result.Item1;
            string? picture = result.Item2;
            if (role == null)
                return BadRequest("Account is wrong! Please try again");
            var token = _tokenService.GenerateToken(requestLogin, role);
            
            return Ok(new AuthResponse
            {
                AccessToken = $"Bearer {token}",
                Picture = picture,
            });
        }
        [HttpPost("login-google")]
        public async Task<IActionResult> LoginByGoogle([FromQuery] string idGG)
        {
            Guid customerId = await _credentialRepo.GetCustomerIdAsync(idGG);
            if (customerId == Guid.Empty)
                return BadRequest(new Response<string>
                {
                    Message = "Invalid GoogleId!"
                });

            string email = await _userRepo.GetEmailByCustomerIdAsync(customerId);
            if (email == null)
                return BadRequest(new Response<string>
                {
                    Message = "Invalid CustomerId!"
                });

            RequestLogin login = new()
            {
                Login = email,
                Password = null
            };

            var result = await _loginService.LoginAsync(login);
            string role = result.Item1;
            string? picture = result.Item2;
            if (role == null)
                return BadRequest("Account is wrong! Please try again");
            var token = _tokenService.GenerateToken(login, role);

            return Ok(new AuthResponse
            {
                AccessToken = $"Bearer {token}",
                Picture = picture,
            });
        }
    }
}