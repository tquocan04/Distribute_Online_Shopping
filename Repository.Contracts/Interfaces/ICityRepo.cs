using Entities.Entities;
using Online_Shopping.Context;

namespace Repository.Contracts.Interfaces
{
    public interface ICityRepo
    {
        Task<IEnumerable<City>> GetAllCitiesAsync();
        Task<IEnumerable<District>> GetDistrictsByCityIdAsync(Guid cityId);
        Task<City> GetCityByCityIdAsync(Guid cityId);
    }
}
