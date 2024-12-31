using AutoMapper;
using DTOs.DTOs;
using DTOs.Request;
using Entities.Entities;
using Repository.Contracts;
using Repository.Contracts.Interfaces;
using Service.Contracts.Interfaces;

namespace Services.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepo _categoryRepo;
        private readonly IMapper _mapper;

        public CategoryService(ICategoryRepo categoryRepo, IMapper mapper)
        {
            _categoryRepo = categoryRepo;
            _mapper = mapper;
        }


        public async Task<Category> CreateNewCategory(RequestCategory requestCategory)
        {
            Category category = new Category();
            _mapper.Map(requestCategory, category);
            await _categoryRepo.CreateNewCategoryAsync(category);

            category = _mapper.Map(requestCategory, category);
            
            return category;
        }

        public async Task DeleteCategoryById(string Id)
        {
            await _categoryRepo.DeleteCategoryByIdAsync(Guid.Parse(Id));
        }

        public async Task<IEnumerable<CategoryDTO>> GetAllCategory()
        {
            var allCategories = await _categoryRepo.GetAllCategoryAsync();
            
            return _mapper.Map<IEnumerable<CategoryDTO>>(allCategories);
        }

        public async Task<CategoryDTO> GetCategoryById(string Id)
        {
            var category = await _categoryRepo.GetCategoryByIdAsync(Guid.Parse(Id));
            return _mapper.Map<CategoryDTO>(category);
        }

        public async Task<bool> UpdateCategoryById(string Id, RequestCategory requestCategory)
        {
            var category = await _categoryRepo.GetCategoryByIdAsync(Guid.Parse(Id));
            if (category == null)
            {
                return false;
            }
            
            _mapper.Map(requestCategory, category); // dto -> category

            return await _categoryRepo.UpdateCategoryAsync(category); 

        }
    }
}
