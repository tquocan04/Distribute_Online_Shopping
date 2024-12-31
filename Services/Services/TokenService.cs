using AutoMapper;
using Service.Contracts.Interfaces;
using System.Security.Claims;
using Microsoft.Extensions.Configuration;
using DTOs.DTOs;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using DTOs.Request;
using Microsoft.AspNetCore.Http;
using Repository.Contracts.Interfaces;

namespace Services.Services
{
    public class TokenService : ITokenService
    {
        private readonly ILoginRepo _loginRepo;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public TokenService(IConfiguration configuration, IHttpContextAccessor httpContextAccessor,
            ILoginRepo loginRepo)
        {
            _loginRepo = loginRepo;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }

        private List<Claim> GetClaim(RequestLogin requestLogin, string role)
        {
            var claims = new List<Claim> 
            {
                new Claim(ClaimTypes.Name, requestLogin.Login),
                new Claim(ClaimTypes.Role, role)
                // Add additional claims as needed (Id, email, role,...)
            };
            return claims;
        }

        public string GenerateToken(RequestLogin requestLogin, string role)
        {
            var expirymonth = int.Parse(_configuration["Jwt:TokenExpiredInMonth"]);

            
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Secret"]));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            var tokeOptions = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: GetClaim(requestLogin, role),
                expires: DateTime.Now.AddMonths(expirymonth),
                signingCredentials: signinCredentials
            );
            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
            return tokenString;
        }

        public async Task<Guid> GetIdCustomerByToken()
        {
            var userNameClaim = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.Name);
            var email = userNameClaim?.Value;
            Guid id = await _loginRepo.GetCustomerIdFromEmail(email);
            return id;
        }

        public async Task<Guid> GetIdEmployeeByToken()
        {
            var userNameClaim = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.Name);
            var username = userNameClaim?.Value;
            Guid id = await _loginRepo.GetEmployeeIdFromUsername(username);
            return id;
        }

        public string GetRoleByToken()
        {
            var roleName = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.Role);
            string role = roleName?.Value;
            return role;
        }

        public string GetLoginAccountByToken()
        {
            var name = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.Name);
            string account = name?.Value;
            return account;
        }
    }
}
