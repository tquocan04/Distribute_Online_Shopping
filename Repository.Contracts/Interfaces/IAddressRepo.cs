using Entities.Entities;

namespace Repository.Contracts.Interfaces
{
    public interface IAddressRepo
    {
        Task CreateNewAddress(Address address);
        Task<Address?> GetAddressByObjectIdAsync(Guid objectId);
        Task<List<Address>> GetListAddressOfCustomerAsync(Guid customerId);
        Task DeleteAddress(Address address);
        Task UpdateAddress(Address address);
        Task<Guid> GetCityIdByDistrictIdAsync(Guid districtId);
        Task<string> GetRegionIdByCityIdAsync(Guid cityId);
        Task<string> GetRegionNameByRegionIdAsync(string regionId);
        Task<string> GetCityNameByCityIdAsync(Guid cityId);
        Task<string> GetDistrictNameByDistrictIdAsync(Guid districtId);
    }
}
