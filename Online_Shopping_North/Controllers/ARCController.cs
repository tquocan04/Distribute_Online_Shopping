using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Online_Shopping_North.DTOs;
using Online_Shopping_North.Repository.Contracts;
using Online_Shopping_North.Requests;
using Online_Shopping_North.Service.Contracts;

namespace Online_Shopping_North.Controllers
{
    [Route("api/arc-shop/north")]
    [ApiController]
    public class ARCController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public ARCController(IEmployeeService employeeRepo)
        {
            _employeeService = employeeRepo;
        }

        [HttpPost("new-staff/{Id}")]
        public async Task<IActionResult> CreateNewStaff(Guid Id, [FromBody] RequestEmployee requestEmployee)
        {
            await _employeeService.AddNewEmployee(Id, requestEmployee);
            return CreatedAtAction("GetProfile", new { id = Id });
        }

        [HttpDelete("profile/{id}")]
        public async Task DeleteStaff(string id)
        {
            await _employeeService.DeleteEmployee(id);
        }

        [HttpPut("profile/{id}")]
        public async Task UpdateProfileStaff(string id, [FromBody] RequestEmployee requestEmployee)
        {
            await _employeeService.UpdateProfile(id, requestEmployee);
        }

        [HttpGet("profile/{id}")]
        public async Task<IActionResult> GetProfile(string id)
        {
            var profile = await _employeeService.GetProfileEmployee(id);
            return Ok(profile);
        }
    }
}
