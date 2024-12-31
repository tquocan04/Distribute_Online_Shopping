using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Online_Shopping_Central.Requests;
using Online_Shopping_Central.Service.Contracts;

namespace Online_Shopping_Central.Controllers
{
    [Route("api/arc-shop/central")]
    [ApiController]
    public class ARCController(IEmployeeService employeeRepo) : ControllerBase
    {
        private readonly IEmployeeService _employeeService = employeeRepo;

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
