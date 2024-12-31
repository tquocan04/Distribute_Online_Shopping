using AutoMapper;
using DTOs.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repositories.Repositories;
using Repository.Contracts.Interfaces;

namespace Online_Shopping.Controllers
{
    [Route("api/regions")]
    [ApiController]
    public class RegionController : ControllerBase
    {
        private readonly IRegionRepo _regionRepo;
        private readonly IMapper _mapper;

        public RegionController(IRegionRepo regionRepo, IMapper mapper) 
        {
            _regionRepo = regionRepo;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRegions() 
        {
            var regions = await _regionRepo.GetAllRegions();
            var dto = _mapper.Map<IEnumerable<RegionDTO>>(regions);
            return Ok(dto);
        }
    }
}
