using DTOs.Request;

namespace Service.Contracts.Interfaces
{
    public interface ILoginService
    {
        //<Role, Picture>
        Task<Tuple<string, string?>> LoginAsync(RequestLogin requestLogin);
    }
}
