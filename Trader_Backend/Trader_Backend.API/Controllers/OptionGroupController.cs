using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ClientModel.Primitives;
using Trader_Backend.API.Extensions;
using Trader_Backend.Application.Common;
using Trader_Backend.Application.DTOs;
using Trader_Backend.Application.Interfaces.Services;
namespace Trader_Backend.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OptionGroupController : ControllerBase
    {
        private readonly IOptionGroupService _optionGroupService;

        public OptionGroupController(IOptionGroupService optionGroupService)
        {
            _optionGroupService = optionGroupService;
        }

        [HttpGet("{id:int:min(1)}")]
        public async Task<IActionResult> GetOptionGroupById(int id)
        {
            var optionGroup = await _optionGroupService.GetOptionGroupByIdAsync(id);
            if(optionGroup == null)
            {
                return this.ToActionResult(OperationResult<OptionGroupViewDto>.Failure(ApplicationErrorCode.NotFound, "Option group not found."));
            }
            return this.ToActionResult(OperationResult<OptionGroupViewDto>.Success(optionGroup));
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> GetAllOptionGroups()
        {
            var optionGroups = await _optionGroupService.GetAllOptionGroupsAsync();
            return this.ToActionResult(OperationResult<IEnumerable<OptionGroupViewDto>>.Success(optionGroups));
        }

        [HttpPost("add")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddOptionGroup([FromBody] OptionGroupDto optionGroup)
        {
            if (optionGroup == null)
            {
                return this.ToActionResult(OperationResult<OptionGroupDto>.Failure(ApplicationErrorCode.InvalidInput, "Option group data is required."));
            }
            var result = await _optionGroupService.AddOptionGroupAsync(optionGroup);
            return this.ToActionResult(result);
        }
        [HttpPut("update/{id:int:min(1)}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateOptionGroup(int id, [FromBody] OptionGroupDto optionGroup)
        {
            var result = await _optionGroupService.UpdateOptionGroupAsync(id, optionGroup);
            
            if(!result.IsSuccess)
            {
                return this.ToActionResult(result);
            }
            return this.ToActionResult(result);
        }
        [HttpDelete("delete/{id:int:min(1)}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteOptionGroup(int id)
        {
            var result = await _optionGroupService.DeleteOptionGroupAsync(id);
            if (!result.IsSuccess)
            {
                return this.ToActionResult(result);
            }
            return this.ToActionResult(result);
        }
    }
}
