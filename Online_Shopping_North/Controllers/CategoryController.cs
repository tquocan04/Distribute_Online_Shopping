using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Online_Shopping_North.Entities;
using Online_Shopping_North.Repository.Contracts;
using Online_Shopping_North.Requests;
using Online_Shopping_North.Responses;
using Online_Shopping_North.Service.Contracts;

namespace Online_Shopping_North.Controllers
{
    [Route("api/categories/north")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly ICategoryRepo _categoryRepo;

        public CategoryController(ICategoryService categoryService,
            ICategoryRepo categoryRepo)
        {
            _categoryService = categoryService;
            _categoryRepo = categoryRepo;
        }

        [HttpPost("new-category")]
        public async Task<IActionResult> AddNewCategory([FromBody] Category category)
        {
            await _categoryRepo.CreateNewCategoryAsync(category);

            return CreatedAtAction(nameof(AddNewCategory), new { id = category.Id });
        }

        [HttpDelete("{Id}")]
        public async Task<ActionResult> DeleteCategoryById(string Id)
        {
            await _categoryService.DeleteCategoryById(Id);
            return NoContent();
        }

        [HttpPatch("{Id}")]
        public async Task<ActionResult> UpdateCategory(Guid Id, [FromBody] RequestCategory request)
        {
            await _categoryService.UpdateCategoryById(Id, request);
            return Ok();
        }

    }
}
