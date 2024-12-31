using AutoMapper;
using DTOs.DTOs;
using DTOs.Request;
using DTOs.Responses;
using Entities.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository.Contracts.Interfaces;
using Service.Contracts.Interfaces;
using Services.Services;
using System.Net.Http;

namespace Online_Shopping.Controllers
{
    [Route("api/arc-shop")]
    [ApiController]
    [Authorize(Roles = "Admin, Staff")]
    public class ARCController : ControllerBase
    {
        private readonly IUserRepo _userRepo;
        private readonly ILoginRepo _loginRepo;
        private readonly IEmployeeService _employeeService;
        private readonly ITokenService _tokenService;
        private readonly IAddressService<Employee> _addressService;
        private readonly HttpClient _httpClient;

        private readonly string apiNorth = "http://localhost:5285/api/arc-shop/north";
        private readonly string apiSouth = "http://localhost:5125/api/arc-shop/south";
        private readonly string apiCentral = "http://localhost:5257/api/arc-shop/central";

        public ARCController(IUserRepo userRepo, 
            ILoginRepo loginRepo,
            ITokenService tokenService,
            IAddressService<Employee> addressService,
            HttpClient httpClient,
            IEmployeeService employeeRepo)
        {
            _userRepo = userRepo;
            _loginRepo = loginRepo;
            _employeeService = employeeRepo;
            _tokenService = tokenService;
            _addressService = addressService;
            _httpClient = httpClient;
        }

        [HttpPost("new-staff")]
        public async Task<IActionResult> CreateNewStaff([FromBody] RequestEmployee requestEmployee)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            else if (await _userRepo.checkEmailExist(requestEmployee.Email))
                return BadRequest("Email is existed");

            else if (!_userRepo.checkDOB(requestEmployee.Year))
                return BadRequest("DoB is invalid");

            EmployeeDTO empDTO = await _employeeService.AddNewEmployee(requestEmployee);

            if (requestEmployee.RegionId == "Bac")
                await _httpClient.PostAsJsonAsync($"{apiNorth}/new-staff/{empDTO.Id}", requestEmployee);
            
            else if (requestEmployee.RegionId == "Nam")
                await _httpClient.PostAsJsonAsync($"{apiSouth}/new-staff/{empDTO.Id}", requestEmployee);

            else
                await _httpClient.PostAsJsonAsync($"{apiCentral}/new-staff/{empDTO.Id}", requestEmployee);

            return CreatedAtAction("GetProfile", new { id = empDTO.Id }, empDTO);
        }

        [HttpDelete("profile")]
        public async Task<IActionResult> DeleteStaff()
        {
            Guid id = await _tokenService.GetIdEmployeeByToken();
            var emp = await _employeeService.GetProfileEmployee(id);

            if (emp != null)
            {
                await _employeeService.DeleteEmployee(id);

                if (emp.RegionId == "Bac")
                    await _httpClient.DeleteAsync($"{apiNorth}/profile/{id}");
                else if (emp.RegionId == "Nam")
                    await _httpClient.DeleteAsync($"{apiSouth}/profile/{id}");
                else
                    await _httpClient.DeleteAsync($"{apiCentral}/profile/{id}");
            }
            else
                return NotFound(new Response<string>
                {
                    Message = "This staff does not exist!"
                });

            return NoContent();
        }
        
        [HttpPut("profile")]
        public async Task<IActionResult> UpdateProfileStaff([FromBody] RequestEmployee requestEmployee)
        {
            Guid id = await _tokenService.GetIdEmployeeByToken();
            var check = await _employeeService.UpdateProfile(id, requestEmployee);
            if (!check)
                return BadRequest(new Response<string>
                {
                    Message = "Invalid information!"
                });

            string currentRegion = await _addressService.GetRegionIdOfObject(id);
            if (currentRegion == "Bac")
            {
                if (requestEmployee.RegionId == "Bac")
                    await _httpClient.PutAsJsonAsync($"{apiNorth}/profile/{id}", requestEmployee);
                else
                {
                    await _httpClient.DeleteAsync($"{apiNorth}/profile/{id}");
                    if (requestEmployee.RegionId == "Nam")
                        await _httpClient.PostAsJsonAsync($"{apiSouth}/new-staff/{id}", requestEmployee);
                    //Trung
                    else if (requestEmployee.RegionId == "Trung")
                        await _httpClient.PostAsJsonAsync($"{apiCentral}/new-staff/{id}", requestEmployee);
                }
            }
            else if (currentRegion == "Nam")
            {
                if (requestEmployee.RegionId == "Nam")
                    await _httpClient.PutAsJsonAsync($"{apiSouth}/profile/{id}", requestEmployee);
                else
                {
                    await _httpClient.DeleteAsync($"{apiSouth}/profile/{id}");
                    if (requestEmployee.RegionId == "Bac")
                        await _httpClient.PostAsJsonAsync($"{apiNorth}/new-staff/{id}", requestEmployee);
                    //Trung
                    else if (requestEmployee.RegionId == "Trung")
                        await _httpClient.PostAsJsonAsync($"{apiCentral}/new-staff/{id}", requestEmployee);
                }
            }
            //Trung
            else if (currentRegion == "Trung")
            {
                if (requestEmployee.RegionId == "Trung")
                    await _httpClient.PutAsJsonAsync($"{apiCentral}/profile/{id}", requestEmployee);

                else
                {
                    await _httpClient.DeleteAsync($"{apiCentral}/profile/{id}");
                    if (requestEmployee.RegionId == "Bac")
                        await _httpClient.PostAsJsonAsync($"{apiNorth}/new-staff/{id}", requestEmployee);
                    //Nam
                    else if (requestEmployee.RegionId == "Nam")
                        await _httpClient.PostAsJsonAsync($"{apiSouth}/new-staff/{id}", requestEmployee);
                }
            }
            return NoContent();
        }

        [HttpGet("profile")]
        public async Task<IActionResult> GetProfile()
        {
            Guid id = await _tokenService.GetIdEmployeeByToken();
            var profile = await _employeeService.GetProfileEmployee(id);

            if (profile == null)
                return NotFound(new Response<string>
                {
                    Message = "This staff does not exist!"
                });

            else
            {
                if (profile.RegionId == "Bac")
                {
                    var response = await _httpClient.GetAsync($"{apiNorth}/profile/{id}");
                    if (response.IsSuccessStatusCode)
                    {
                        return Ok(new Response<EmployeeDTO>
                        {
                            Message = "Profile from North",
                            Data = await response.Content.ReadFromJsonAsync<EmployeeDTO>()
                        });
                    }
                }
                else if (profile.RegionId == "Nam")
                {
                    var response = await _httpClient.GetAsync($"{apiSouth}/profile/{id}");
                    if (response.IsSuccessStatusCode)
                    {
                        return Ok(new Response<EmployeeDTO>
                        {
                            Message = "Profile from South",
                            Data = await response.Content.ReadFromJsonAsync<EmployeeDTO>()
                        });
                    }
                }
                else if (profile.RegionId == "Trung")
                {
                    var response = await _httpClient.GetAsync($"{apiCentral}/profile/{id}");
                    if (response.IsSuccessStatusCode)
                    {
                        return Ok(new Response<EmployeeDTO>
                        {
                            Message = "Profile from Central",
                            Data = await response.Content.ReadFromJsonAsync<EmployeeDTO>()
                        });
                    }
                }
            }

            return Ok(profile);
        }
    }
}
