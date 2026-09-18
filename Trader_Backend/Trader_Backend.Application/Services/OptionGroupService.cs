using System;
using System.Collections.Generic;
using System.Text;
using Trader_Backend.Application.Interfaces.Repositories;
using Trader_Backend.Application.DTOs;
using Trader_Backend.Domain.Entities;
using Trader_Backend.Application.Common;
using Trader_Backend.Application.Interfaces.Services;
namespace Trader_Backend.Application.Services
{
    public class OptionGroupService : IOptionGroupService
    {
        private readonly IOptionGroupRepository _optionGroupRepository;

        public OptionGroupService(IOptionGroupRepository optionGroupRepository)
        {
            _optionGroupRepository = optionGroupRepository;
        }

        public async Task<OptionGroupViewDto?> GetOptionGroupByIdAsync(int id)
        {
            var optionGroup = await _optionGroupRepository.GetByIdAsync(id);
            if (optionGroup == null) return null;

            return new OptionGroupViewDto
            {
                ID = optionGroup.ID,
                Value = optionGroup.Value,
                CategoryID = optionGroup.CategoryID
            };
        }

        public async Task<IEnumerable<OptionGroupViewDto>> GetAllOptionGroupsAsync()
        {
            var optionGroups = await _optionGroupRepository.GetAllAsync();
            return optionGroups.Select(og => new OptionGroupViewDto
            {
                ID = og.ID,
                Value = og.Value,
                CategoryID = og.CategoryID
            });
        }

        public async Task<OperationResult<int>> AddOptionGroupAsync(OptionGroupDto optionGroup)
        {
            var optionGroupEntity = new OptionGroup
            {
                Value = optionGroup.Value,
                CategoryID = optionGroup.CategoryID
            };
            await _optionGroupRepository.AddAsync(optionGroupEntity);
            return OperationResult<int>.Success(optionGroupEntity.ID);
        }

        public async Task<OperationResult<bool>> UpdateOptionGroupAsync(int id, OptionGroupDto optionGroup)
        {
            var existingOptionGroup = await _optionGroupRepository.GetByIdAsync(id);
            if (existingOptionGroup == null)
            {
                return OperationResult<bool>.Failure(ApplicationErrorCode.NotFound,"Option group not found.");
            }
            existingOptionGroup.Value = optionGroup.Value;
            existingOptionGroup.CategoryID = optionGroup.CategoryID;
            await _optionGroupRepository.UpdateAsync(existingOptionGroup);
            return OperationResult<bool>.Success(true);
        }

        public async Task<OperationResult<bool>> DeleteOptionGroupAsync(int id)
        {
            var existingOptionGroup = await _optionGroupRepository.GetByIdAsync(id);
            if (existingOptionGroup == null)
            {
                return OperationResult<bool>.Failure(ApplicationErrorCode.NotFound, "Option group not found.");
            }
            await _optionGroupRepository.DeleteAsync(existingOptionGroup);
            return OperationResult<bool>.Success(true);
        }
    }
}
