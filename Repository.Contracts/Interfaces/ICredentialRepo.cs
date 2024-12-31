using Entities.Entities;

namespace Repository.Contracts.Interfaces
{
    public interface ICredentialRepo
    {
        Task CreateCredentialAsync(Credential credential);
        Task<Credential?> GetIdCredentialAsync(string id);
        Task<Guid> GetCustomerIdAsync(string id);
        Task<string> GetCredentialIdByCustomerIdAsync(Guid id);
    }
}
