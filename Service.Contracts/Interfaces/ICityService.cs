using DTOs.DTOs;
using Entities.Entities;
using Online_Shopping.Context;

namespace Service.Contracts.Interfaces
{
    public interface ICityService
    {
        Task<IEnumerable<CityDTO>> GetAllCities();
        Task<IEnumerable<DistrictDTO>> GetDistrictsByCityId(Guid cityId);
    }
}
