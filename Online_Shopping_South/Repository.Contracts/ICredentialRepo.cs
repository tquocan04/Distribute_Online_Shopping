using Online_Shopping_South.Entities;

namespace Online_Shopping_South.Repository.Contracts
{
    public interface ICredentialRepo
    {
        Task CreateCredentialAsync(Credential credential);
        Task<Guid> GetCustomerIdAsync(string id);
    }
}
