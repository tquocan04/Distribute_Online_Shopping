namespace Repository.Contracts.Interfaces
{
    public interface ILoginRepo
    {
        Task<string> checkLoginCustomerAsync(string login, string password);
        Task<string> checkLoginEmployeeAsync(string login, string password);
        Task<Guid> GetCustomerIdFromEmail(string account);
        Task<Guid> GetEmployeeIdFromUsername(string account);
    }
}