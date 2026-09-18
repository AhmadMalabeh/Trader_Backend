using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Trader_Backend.API.Extensions;
using Trader_Backend.Application.Common;
using Trader_Backend.Application.DTOs;
using Trader_Backend.Application.Interfaces.Services;
namespace Trader_Backend.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        
        [HttpGet("{id:int:min(1)}")]
        public async Task<IActionResult> GetCategoryById(int id)
        {
            var category = await _categoryService.GetCategoryByIdAsync(id);
            if (category == null)
            {
                return this.ToActionResult(OperationResult<CategoryViewDto>.Failure(ApplicationErrorCode.NotFound, "Category not found."));
            }
            return this.ToActionResult(OperationResult<CategoryViewDto>.Success(category));
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> GetAllCategories()
        {
            var categories = await _categoryService.GetAllCategoriesAsync();
            return this.ToActionResult(OperationResult<IEnumerable<CategoryViewDto>>.Success(categories));
        }

        [HttpPost("add")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddCategory([FromBody] CategoryDto category)
        {
            if (category == null)
            {
                return this.ToActionResult(OperationResult<CategoryDto>.Failure(ApplicationErrorCode.InvalidInput, "Category data is required."));
            }
            await _categoryService.AddCategoryAsync(category);
            return this.ToActionResult(OperationResult<CategoryDto>.Success(category));
        }

        [HttpPut("update/{id:int:min(1)}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateCategory(int id, [FromBody] CategoryDto category)
        {
           var result = await _categoryService.UpdateCategoryAsync(id, category);
            if (!result.IsSuccess)
            {
                return this.ToActionResult(result);
            }
            return this.ToActionResult(result);
        }

        [HttpDelete("delete/{id:int:min(1)}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var result = await _categoryService.DeleteCategoryAsync(id);
            if (!result.IsSuccess)
            {
                return this.ToActionResult(result);
            }
            return this.ToActionResult(result);
        }

    }
}
