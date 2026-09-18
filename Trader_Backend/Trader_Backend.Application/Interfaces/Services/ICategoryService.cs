using System;
using System.Collections.Generic;
using System.Text;
using Trader_Backend.Application.Common;
using Trader_Backend.Application.DTOs;

namespace Trader_Backend.Application.Interfaces.Services
{
    public interface ICategoryService
    {
        Task<CategoryViewDto?> GetCategoryByIdAsync(int id);
        Task<IEnumerable<CategoryViewDto>> GetAllCategoriesAsync();
        Task<OperationResult<int>> AddCategoryAsync(CategoryDto category);
        Task<OperationResult<bool>> UpdateCategoryAsync(int id, CategoryDto category);
        Task<OperationResult<bool>> DeleteCategoryAsync(int id);
    }
}
