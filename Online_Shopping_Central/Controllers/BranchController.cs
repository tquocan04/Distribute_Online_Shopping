using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Online_Shopping_Central.DTOs;
using Online_Shopping_Central.Repository.Contracts;
using Online_Shopping_Central.Requests;
using Online_Shopping_Central.Service.Contracts;

namespace Online_Shopping_Central.Controllers
{
    [Route("api/branches/central")]
    [ApiController]
    public class BranchController : ControllerBase
    {
        private readonly IBranchService _branchService;
        private readonly IMapper _mapper;
        private readonly IAddressRepo _addressRepo;

        public BranchController(IBranchService branchService, IMapper mapper,
            IAddressRepo addressRepo)
        {
            _branchService = branchService;
            _mapper = mapper;
            _addressRepo = addressRepo;
        }

        [HttpPost("new-branch/{id}")]
        public async Task<IActionResult> AddNewBranch(string id, [FromBody] RequestBranch requestBranch)
        {
            BranchDTO branchDTO = await _branchService.AddNewBranch(id, requestBranch);

            return CreatedAtAction(nameof(GetAllBranches), new { id = branchDTO.Id }, requestBranch);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBranches()
        {
            var list = await _branchService.GetBranchList();
            if (list == null)
            {
                return NotFound();
            }
            return Ok(list);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBranch(string id)
        {
            var branch = await _branchService.GetBranch(id);
            if (branch == null)
            {
                return NotFound();
            }
            return Ok(branch);
        }

        [HttpDelete("{id}")]
        public async Task DeleteBranch(string id)
        {
            await _branchService.DeleteBranch(id);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBranch(string id, [FromBody] RequestBranch requestBranch)
        {
            await _branchService.UpdateBranch(id, requestBranch);
            return NoContent();
        }
    }
}
