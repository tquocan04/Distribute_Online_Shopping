using AutoMapper;
using DTOs.DTOs;
using DTOs.Request;
using DTOs.Responses;
using Entities.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repositories.Repositories;
using Repository.Contracts.Interfaces;
using Service.Contracts.Interfaces;
using Services.Services;
using System.Net.Http;

namespace Online_Shopping.Controllers
{
    [Route("api/branches")]
    [ApiController]
    public class BranchController : ControllerBase
    {
        private readonly IBranchService _branchService;
        private readonly IMapper _mapper;
        private readonly HttpClient _httpClient;
        private readonly IAddressRepo _addressRepo;
        private readonly IAddressService<Branch> _addressService;
        private readonly ITokenService _tokenService;
        private readonly string apiNorth = "http://localhost:5285/api/branches/north";
        private readonly string apiSouth = "http://localhost:5125/api/branches/south";
        //private readonly string apiCentral = "http://localhost:5257/api/branches/south";

        public BranchController(IBranchService branchService, IMapper mapper,
            HttpClient httpClient, IAddressService<Branch> addressService,
            ITokenService tokenService,
            IAddressRepo addressRepo)
        {
            _branchService = branchService;
            _mapper = mapper;
            _httpClient = httpClient;
            _addressRepo = addressRepo;
            _addressService = addressService;
            _tokenService = tokenService;
        }

        [HttpPost("new-branch")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddNewBranch([FromBody] RequestBranch requestBranch)
        {
            BranchDTO branchDTO = await _branchService.AddNewBranch(requestBranch);

            if (requestBranch.RegionId == "Bac")
            {
                await _httpClient.PostAsJsonAsync($"{apiNorth}/new-branch/{branchDTO.Id}", requestBranch);
            }
            else if (requestBranch.RegionId == "Nam")
                await _httpClient.PostAsJsonAsync($"{apiSouth}/new-branch/{branchDTO.Id}", requestBranch);
            //else if (requestBranch.RegionId == "Nam")
            //    await _httpClient.PostAsJsonAsync($"{apiCentral}/new-branch/{branchDTO.Id}", requestBranch);

            return CreatedAtAction(nameof(GetBranch), new { id = branchDTO.Id }, requestBranch);
        }

        [HttpGet]
        [Authorize(Roles = "Admin, Staff")]
        public async Task<IActionResult> GetAllBranches()
        {
            Guid id = await _tokenService.GetIdEmployeeByToken();
            string region = await _addressService.GetRegionIdOfObject(id);

            if (region == "Bac")
            {
                var response = await _httpClient.GetAsync($"{apiNorth}");
                if (response.IsSuccessStatusCode)
                {
                    return Ok(new Response<List<BranchDTO>>
                    {
                        Message = "Branches list from North",
                        Data = await response.Content.ReadFromJsonAsync<List<BranchDTO>>()
                    });
                }
            }

            else if (region == "Nam")
            {
                var response = await _httpClient.GetAsync($"{apiSouth}");
                if (response.IsSuccessStatusCode)
                {
                    return Ok(new Response<List<BranchDTO>>
                    {
                        Message = "Branches list from South",
                        Data = await response.Content.ReadFromJsonAsync<List<BranchDTO>>()
                    });
                }
            }
            
            //else if (region == "Trung")
            //{
            //    var response = await _httpClient.GetAsync($"{apiCentral}");
            //    if (response.IsSuccessStatusCode)
            //    {
            //        return Ok(new Response<List<BranchDTO>>
            //        {
            //            Message = "Branches list from Central",
            //            Data = await response.Content.ReadFromJsonAsync<List<BranchDTO>>()
            //        });
            //    }
            //}

            var list = await _branchService.GetBranchList();
            if (list.Count == 0)
            {
                return NotFound(new Response<List<BranchDTO>>
                {
                    Message = "Does not have any branches!",
                    Data = list
                });
            }
            return Ok(list);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBranch(string id)
        {
            string region = await _addressService.GetRegionIdOfObject(Guid.Parse(id));
            if (region == "Bac")
            {
                var response = await _httpClient.GetAsync($"{apiNorth}/{id}");
                if (response.IsSuccessStatusCode)
                {
                    return Ok(new Response<BranchDTO>
                    {
                        Message = "Detail branch in North",
                        Data = await response.Content.ReadFromJsonAsync<BranchDTO>()
                    });
                       
                }
            }
            else if (region == "Nam")
            {
                var response = await _httpClient.GetAsync($"{apiSouth}/{id}");
                if (response.IsSuccessStatusCode)
                {
                    return Ok(new Response<BranchDTO>
                    {
                        Message = "Detail branch in South",
                        Data = await response.Content.ReadFromJsonAsync<BranchDTO>()
                    });
                       
                }
            }
            //else if (region == "Trung")
            //{
            //    var response = await _httpClient.GetAsync($"{apiCentral}/{id}");
            //    if (response.IsSuccessStatusCode)
            //    {
            //        return Ok(new Response<BranchDTO>
            //        {
            //            Message = "Detail branch in South",
            //            Data = await response.Content.ReadFromJsonAsync<BranchDTO>()
            //        });
                       
            //    }
            //}

            var branch = await _branchService.GetBranch(id);
            
            if (branch == null)
            {
                return NotFound(new Response<string>
                {
                    Message = "This branch does not exist!"
                });
            }

            return Ok(branch);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteBranch(string id)
        {
            var branch = await _branchService.GetBranch(id);
            if (branch == null)
            {
                return NotFound(new Response<string>
                {
                    Message = "This branch does not exist!"
                });
            }

            await _branchService.DeleteBranch(id);
            if (branch.RegionId == "Bac")
                await _httpClient.DeleteAsync($"{apiNorth}/{id}");

            else if (branch.RegionId == "Nam")
                await _httpClient.DeleteAsync($"{apiSouth}/{id}");

            //else if (branch.RegionId == "Trung")
            //    await _httpClient.DeleteAsync($"{apiSouth}/{id}");
            
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBranch(string id, [FromBody] RequestBranch requestBranch)
        {
            if (await _branchService.GetBranch(id) == null)
                return NotFound(new Response<string>
                {
                    Message = "This branch does not exist!"
                });

            string currentRegionId = await _addressService.GetRegionIdOfObject(Guid.Parse(id));
            await _branchService.UpdateBranch(id, requestBranch);

            if (currentRegionId == "Bac")
            {
                if (requestBranch.RegionId == "Bac")
                    await _httpClient.PutAsJsonAsync($"{apiNorth}/{id}", requestBranch);
                
                else
                {
                    await _httpClient.DeleteAsync($"{apiNorth}/{id}");
                    if (requestBranch.RegionId == "Nam")
                        await _httpClient.PostAsJsonAsync($"{apiSouth}/new-branch/{id}", requestBranch);
                    //Trung
                    //else if (requestBranch.RegionId == "Trung")
                    //    await _httpClient.PostAsJsonAsync($"{apiCentral}/new-branch/{id}", requestBranch);
                }
            }
            else if (currentRegionId == "Nam")
            {
                if (requestBranch.RegionId == "Nam")
                    await _httpClient.PutAsJsonAsync($"{apiSouth}/{id}", requestBranch);

                else
                {
                    await _httpClient.DeleteAsync($"{apiSouth}/{id}");
                    if (requestBranch.RegionId == "Bac")
                        await _httpClient.PostAsJsonAsync($"{apiNorth}/new-branch/{id}", requestBranch);
                    //Trung
                    //else if (requestBranch.RegionId == "Trung")
                    //    await _httpClient.PostAsJsonAsync($"{apiCentral}/new-branch/{id}", requestBranch);
                }
            }
            //else if (currentRegionId == "Trung")
            //{
            //    if (requestBranch.RegionId == "Trung")
            //        await _httpClient.PostAsJsonAsync($"{apiCentral}/new-branch/{id}", requestBranch);

            //    else
            //    {
            //        await _httpClient.DeleteAsync($"{apiCentral}/{id}");
            //        if (requestBranch.RegionId == "Bac")
            //            await _httpClient.PostAsJsonAsync($"{apiNorth}/new-branch/{id}", requestBranch);
                    
            //        else if (requestBranch.RegionId == "Nam")
            //            await _httpClient.PostAsJsonAsync($"{apiSouth}/new-branch/{id}", requestBranch);
            //    }
            //}
            return NoContent();
        }
    }
}
