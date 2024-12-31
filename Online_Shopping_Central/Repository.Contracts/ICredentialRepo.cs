using Online_Shopping_Central.Entities;

namespace Online_Shopping_Central.Repository.Contracts
{
    public interface ICredentialRepo
    {
        Task CreateCredentialAsync(Credential credential);
        Task<Guid> GetCustomerIdAsync(string id);
    }
}
