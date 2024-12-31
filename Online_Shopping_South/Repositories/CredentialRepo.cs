using Microsoft.EntityFrameworkCore;
using Online_Shopping_South.Repository.Contracts;
using Online_Shopping_South.Entities;

namespace Online_Shopping_South.Repositories
{
    public class CredentialRepo(ApplicationContext applicationContext) : ICredentialRepo
    {
        private readonly ApplicationContext _applicationContext = applicationContext;

        public async Task CreateCredentialAsync(Credential credential)
        {
            _applicationContext.Credentials.Add(credential);
            await _applicationContext.SaveChangesAsync();
        }

        public async Task<Guid> GetCustomerIdAsync(string id)
        {
            return await _applicationContext.Credentials
                .AsNoTracking()
                .Where(x => x.Id == id)
                .Select(x => x.CustomerId)
                .FirstOrDefaultAsync();
        }
    }
}
