using Online_Shopping_North.Entities;

namespace Online_Shopping_North.Repository.Contracts
{
    public interface ICredentialRepo
    {
        Task CreateCredentialAsync(Credential credential);
        Task<Guid> GetCustomerIdAsync(string id);
    }
}
