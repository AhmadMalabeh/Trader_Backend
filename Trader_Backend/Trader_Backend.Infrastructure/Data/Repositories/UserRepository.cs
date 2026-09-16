using System;
using System.Collections.Generic;
using System.Text;
using Trader_Backend.Domain.Entities;
using Trader_Backend.Application.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using Trader_Backend.Application.DTOs;
using Trader_Backend.Application.Common;
namespace Trader_Backend.Infrastructure.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            
            _context = context;
        }

        public async Task<User?> GetUserByIdAsync(int userId)
        {
            return await _context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.ID == userId);
        }

        public async Task<User?> GetUserByEmailAsync(string email)
        {
            return await _context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<User?> GetUserByEntraIdAsync(string entraId)
        {
            return await _context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.EntraID == entraId);
        }

        public async Task UpdateUserAsync( User user)
        {
             _context.Users.Update(user);
        }

        public async Task<PagedResult<User>> GetUsersAsync(int PageNumber, int PageSize)
        {
            var TotalCount = await _context.Users.CountAsync();

            var Users = await _context.Users
                .AsNoTracking()
                .OrderByDescending(u => u.RegisteredAt) 
                .Skip((PageNumber - 1) * PageSize)
                .Take(PageSize)
                .ToListAsync();

            return new PagedResult<User>(Users, TotalCount, PageNumber, PageSize);
        }
        public async Task<InvitationCode?> GetInvitationCodeAsync(string code)
        {
            // فحص الكود ومطابقته مع عمود الـ CodeHash
            return await _context.InvitationCodes.FirstOrDefaultAsync(ic => ic.CodeHash == code);
        }

        public async Task AddUserAsync(User user)
        {
            await _context.Users.AddAsync(user);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
