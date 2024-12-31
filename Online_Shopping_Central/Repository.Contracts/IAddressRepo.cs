using Online_Shopping_Central.Entities;

namespace Online_Shopping_Central.Repository.Contracts
{
    public interface IAddressRepo
    {
        Task CreateNewAddress(Address address);
        Task<Address?> GetAddressByObjectIdAsync(Guid objectId);
        Task UpdateAddress(Address address);
        Task<Guid> GetCityIdByDistrictIdAsync(Guid districtId);
        Task<List<Address>> GetListAddressOfCustomerAsync(Guid customerId);
        Task<string> GetRegionIdByCityIdAsync(Guid cityId);
        Task<string> GetRegionNameByRegionIdAsync(string regionId);
        Task<string> GetCityNameByCityIdAsync(Guid cityId);
        Task<string> GetDistrictNameByDistrictIdAsync(Guid districtId);
    }
}
