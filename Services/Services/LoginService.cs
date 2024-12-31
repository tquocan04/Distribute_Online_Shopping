using DTOs.Request;
using DTOs.Responses;
using Repository.Contracts.Interfaces;
using Service.Contracts.Interfaces;

namespace Services.Services
{
    public class LoginService : ILoginService
    {
        private readonly ILoginRepo _loginRepo;
        private readonly IUserRepo _userRepo;

        public LoginService(ILoginRepo loginRepo, IUserRepo userRepo) 
        {
            _loginRepo = loginRepo;
            _userRepo = userRepo;
        }

        public async Task<Tuple<string, string?>> LoginAsync(RequestLogin requestLogin)
        {
            if (requestLogin.Login.Contains("@"))
            {
                string role = await _loginRepo.checkLoginCustomerAsync(requestLogin.Login, requestLogin.Password); 
                string? picture = await _userRepo.GetPictureOfCustomer(requestLogin.Login);

                return Tuple.Create(role, picture);
            }
            else
            {
                string role = await _loginRepo.checkLoginEmployeeAsync(requestLogin.Login, requestLogin.Password);
                return Tuple.Create(role, (string?)null);
            }

        }
    }
}
