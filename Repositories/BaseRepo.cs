using Online_Shopping.Context;
using Repository.Contracts;

namespace Repositories
{
    public class BaseRepo<T> : IBaseRepo<T> where T : class
    {
        private readonly ApplicationContext _applicationContext;

        public BaseRepo(ApplicationContext applicationContext) 
        {
            _applicationContext = applicationContext;
        }
        public async Task<T?> GetByIdAsync(Guid id)
        {
            return await _applicationContext.Set<T>().FindAsync(id);
        }
    }
}
