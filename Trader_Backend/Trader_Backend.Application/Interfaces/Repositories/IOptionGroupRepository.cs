using System;
using System.Collections.Generic;
using System.Text;
using Trader_Backend.Domain.Entities;

namespace Trader_Backend.Application.Interfaces.Repositories
{
    public interface IOptionGroupRepository
    {
        Task AddAsync(OptionGroup optionGroup);
        Task<OptionGroup?> GetByIdAsync(int id);
        Task<OptionGroup?> GetByValueAsync(string value);
        Task<IEnumerable<OptionGroup>> GetAllAsync();
        Task UpdateAsync(OptionGroup optionGroup);
        Task DeleteAsync(OptionGroup optionGroup);

    }
}
