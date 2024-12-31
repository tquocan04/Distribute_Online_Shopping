using Online_Shopping_North.Repository.Contracts;
using Online_Shopping_North.Service.Contracts;

namespace Online_Shopping_North.Services
{
    public class AddressService<T> : IAddressService<T> where T : class
    {
        private readonly IAddressRepo _addressRepo;

        public AddressService(IAddressRepo addressRepo)
        {
            _addressRepo = addressRepo;
        }

        private void SetProperty(T obj, string propertyName, object value)
        {
            var property = obj.GetType().GetProperty(propertyName);
            if (property != null && property.CanWrite)
            {
                property.SetValue(obj, value);

            }
        }

        public async Task<T> SetAddress(T obj, Guid id)
        {
            var address = await _addressRepo.GetAddressByObjectIdAsync(id);
            SetProperty(obj, "Street", address.Street);
            SetProperty(obj, "DistrictId", address.DistrictId);
            SetProperty(obj, "DistrictName", await _addressRepo.GetDistrictNameByDistrictIdAsync(address.DistrictId));
            SetProperty(obj, "CityId", await _addressRepo.GetCityIdByDistrictIdAsync(address.DistrictId));
            SetProperty(obj, "CityName", await _addressRepo.GetCityNameByCityIdAsync(await _addressRepo.GetCityIdByDistrictIdAsync(address.DistrictId)));
            SetProperty(obj, "RegionId", await _addressRepo.GetRegionIdByCityIdAsync(await _addressRepo.GetCityIdByDistrictIdAsync(address.DistrictId)));
            SetProperty(obj, "RegionName", await _addressRepo.GetRegionNameByRegionIdAsync(await _addressRepo.GetRegionIdByCityIdAsync(await _addressRepo.GetCityIdByDistrictIdAsync(address.DistrictId))));

            return obj;
        }
    }
}
