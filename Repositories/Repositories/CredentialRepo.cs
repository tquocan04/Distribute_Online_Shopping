using Entities.Entities;
using Microsoft.EntityFrameworkCore;
using Online_Shopping.Context;
using Repository.Contracts.Interfaces;

namespace Repositories.Repositories
{
    public class CredentialRepo(ApplicationContext applicationContext) : ICredentialRepo
    {
        private readonly ApplicationContext _applicationContext = applicationContext;

        public async Task CreateCredentialAsync(Credential credential)
        {
            _applicationContext.Credentials.Add(credential);
            await _applicationContext.SaveChangesAsync();
        }

        public async Task<Credential?> GetIdCredentialAsync(string id)
        {
            return await _applicationContext.Credentials.FindAsync(id);
        }

        public async Task<Guid> GetCustomerIdAsync(string id)
        {
            return await _applicationContext.Credentials
                .AsNoTracking()
                .Where(x => x.Id == id)
                .Select(x => x.CustomerId)
                .FirstOrDefaultAsync();
        }

        public async Task<string> GetCredentialIdByCustomerIdAsync(Guid id)
        {
            return await _applicationContext.Credentials
                .AsNoTracking()
                .Where(x => x.CustomerId == id)
                .Select(x => x.Id)
                .FirstOrDefaultAsync();
        }
    }
}
