using AutoMapper;
using DTOs.DTOs;
using Entities.Entities;
using Repository.Contracts.Interfaces;
using Service.Contracts.Interfaces;
using System.Collections.Generic;

namespace Services.Services
{
    public class CityService : ICityService
    {
        private readonly ICityRepo _cityRepo;
        private readonly IMapper _mapper;

        public CityService(ICityRepo cityRepo, IMapper mapper) 
        {
            _cityRepo = cityRepo;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CityDTO>> GetAllCities()
        {
            var cities = await _cityRepo.GetAllCitiesAsync();
            return _mapper.Map<IEnumerable<CityDTO>>(cities);
        }

        public async Task<IEnumerable<DistrictDTO>> GetDistrictsByCityId(Guid cityId)
        {
            var districts = await _cityRepo.GetDistrictsByCityIdAsync(cityId);
            return _mapper.Map<IEnumerable<DistrictDTO>>(districts);
        }
    }
}
