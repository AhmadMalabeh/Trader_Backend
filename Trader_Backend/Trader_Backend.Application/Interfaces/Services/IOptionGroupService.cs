using System;
using System.Collections.Generic;
using System.Text;
using Trader_Backend.Application.Common;
using Trader_Backend.Application.DTOs;

namespace Trader_Backend.Application.Interfaces.Services
{
    public interface IOptionGroupService
    {
        Task<OptionGroupViewDto?> GetOptionGroupByIdAsync(int id);
        Task<IEnumerable<OptionGroupViewDto>> GetAllOptionGroupsAsync();
        Task<OperationResult<int>> AddOptionGroupAsync(OptionGroupDto optionGroup);
        Task<OperationResult<bool>> UpdateOptionGroupAsync(int id, OptionGroupDto optionGroup);
        Task<OperationResult<bool>> DeleteOptionGroupAsync(int id);
    }
}
