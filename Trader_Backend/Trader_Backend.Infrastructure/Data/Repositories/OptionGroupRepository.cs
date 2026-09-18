using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Trader_Backend.Domain.Entities;
using Trader_Backend.Application.Interfaces.Repositories;
namespace Trader_Backend.Infrastructure.Data.Repositories 
{
    public class OptionGroupRepository : IOptionGroupRepository
    {
        private readonly AppDbContext _context;

        public OptionGroupRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(OptionGroup optionGroup)
        {
            _context.OptionGroups.Add(optionGroup);
            await _context.SaveChangesAsync();
        }

        public async Task<OptionGroup?> GetByIdAsync(int id)
        {
            return await _context.OptionGroups.AsNoTracking().FirstOrDefaultAsync(og => og.ID == id);
        }

        public async Task<OptionGroup?> GetByValueAsync(string value)
        {
            return await _context.OptionGroups.AsNoTracking().FirstOrDefaultAsync(og => og.Value == value);
        }

        public async Task<IEnumerable<OptionGroup>> GetAllAsync()
        {
            return await _context.OptionGroups.AsNoTracking().ToListAsync();
        }

        public async Task UpdateAsync(OptionGroup optionGroup)
        {
            _context.OptionGroups.Update(optionGroup);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(OptionGroup optionGroup)
        {
            _context.OptionGroups.Remove(optionGroup);
            await _context.SaveChangesAsync();
        }
    }
}
