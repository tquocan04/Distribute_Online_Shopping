using DTOs.DTOs;
using DTOs.Request;
using Entities.Entities;
using System.Collections.Generic;

namespace Service.Contracts.Interfaces
{
    public interface ICategoryService
    {
        Task<Category> CreateNewCategory(RequestCategory requestCategory);
        Task<IEnumerable<CategoryDTO>> GetAllCategory();
        Task<CategoryDTO> GetCategoryById(string Id);
        Task DeleteCategoryById(string Id);
        Task<bool> UpdateCategoryById(string Id, RequestCategory requestCategory);

    }
}
