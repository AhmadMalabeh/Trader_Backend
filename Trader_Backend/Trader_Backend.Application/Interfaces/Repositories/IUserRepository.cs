using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Trader_Backend.Application.DTOs;
using Trader_Backend.Domain.Entities;
using Trader_Backend.Application.Common;
using System.Security.Cryptography;
namespace Trader_Backend.Application.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task<User?> GetUserByIdAsync(int UserId);
        Task<User?> GetUserByEmailAsync(string Email);
        Task<User?> GetUserByEntraIdAsync(string EntraId);
        Task UpdateUserAsync( User UpdateUser);
        Task<PagedResult<User>> GetUsersAsync(int PageNumber, int PageSize);
        Task<InvitationCode?> GetInvitationCodeAsync(string code); // 👈 إضافة هذا السطر
        Task AddUserAsync(User user); // 👈 إضافة هذا السطر لحفظ مستخدم جديد
        Task SaveChangesAsync(); // 👈 إضافة هذا السطر لحفظ التغييرات في الـ DbContext


    }
}
