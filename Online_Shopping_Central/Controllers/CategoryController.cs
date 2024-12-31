﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Online_Shopping_Central.Entities;
using Online_Shopping_Central.Repository.Contracts;
using Online_Shopping_Central.Requests;
using Online_Shopping_Central.Service.Contracts;

namespace Online_Shopping_Central.Controllers
{
    [Route("api/categories/central")]
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
        public async Task DeleteCategoryById(string Id)
        {
            await _categoryService.DeleteCategoryById(Id);
        }

        [HttpPatch("{Id}")]
        public async Task<ActionResult> UpdateCategory(Guid Id, [FromBody] RequestCategory request)
        {
            await _categoryService.UpdateCategoryById(Id, request);
            return Ok();
        }

    }
}
