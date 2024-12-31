using DTOs.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts.Interfaces;
using Services.Services;

namespace Online_Shopping.Controllers
{
    [Route("api/cities")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly ICityService _cityService;

        public CityController(ICityService cityService) 
        {
            _cityService = cityService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCities()
        {
            var cities = await _cityService.GetAllCities();
            if (cities == null)
            {
                return NotFound();
            }
            return Ok(cities);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetAllDistrictsByCityId(Guid Id)
        {
            var districts = await _cityService.GetDistrictsByCityId(Id);
            if (districts == null)
                return NotFound(); 
            return Ok(districts);
        }
    }
}
