using System;
using System.Collections.Generic;
using System.Text;
using Trader_Backend.Application.Interfaces.Repositories;
using Trader_Backend.Domain.Entities;
using Trader_Backend.Application.DTOs;
using Trader_Backend.Application.Common;
using Trader_Backend.Application.Interfaces.Services;
namespace Trader_Backend.Application.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<CategoryViewDto?> GetCategoryByIdAsync(int id)
        {
           var category = await _categoryRepository.GetByIdAsync(id);
            if (category == null)
            {
                return null;
            }
            return new CategoryViewDto
            {
                ID = category.ID,
                Name = category.Name,
                Description = category.Description
            };

        }

        public async Task<IEnumerable<CategoryViewDto>> GetAllCategoriesAsync()
        {
            var categories = await _categoryRepository.GetAllAsync();
            return categories.Select(c => new CategoryViewDto
            {
                ID = c.ID,
                Name = c.Name,
                Description = c.Description
            });
        }

        public async Task<OperationResult<int>> AddCategoryAsync(CategoryDto category)
        {
            var categoryEntity = new Category
            {
                Name = category.Name,
                Description = category.Description
            };
            await _categoryRepository.AddAsync(categoryEntity);
            return OperationResult<int>.Success(categoryEntity.ID);
        }
        public async Task<OperationResult<bool>> UpdateCategoryAsync(int id, CategoryDto category)
        {
            var existingCategory = await _categoryRepository.GetByIdAsync(id);
            if (existingCategory == null)
            {
                return OperationResult<bool>.Failure(ApplicationErrorCode.NotFound, $"Category with ID {id} not found.");
            }

            Category updatedCategory = new Category
            {
                ID = existingCategory.ID,
                Name = category.Name,
                Description = category.Description
            };

            await _categoryRepository.UpdateAsync(updatedCategory);
            return OperationResult<bool>.Success(true);

        }
        public async Task<OperationResult<bool>> DeleteCategoryAsync(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            if (category == null)
            {
                return OperationResult<bool>.Failure(ApplicationErrorCode.NotFound, $"Category with ID {id} not found.");
            }
            await _categoryRepository.DeleteAsync(category);
            return OperationResult<bool>.Success(true);
        }

    }
}
