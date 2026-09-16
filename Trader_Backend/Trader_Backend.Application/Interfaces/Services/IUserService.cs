using System;
using System.Collections.Generic;
using System.Text;
using Trader_Backend.Application.Common;
using Trader_Backend.Application.DTOs;
using Trader_Backend.Domain.Entities;

namespace Trader_Backend.Application.Interfaces.Services
{
    public interface IUserService
    {
        Task<User?> GetUserByIdAsync(int UserId);
        Task<User?> GetUserByEmailAsync(string Email);
        //Task<User> RegisterUserAsync(RegisterRequestDto RegisterRequest);
        Task<User?> GetUserByEntraIdAsync(string EntraId);
        Task<OperationResult<User>> UpdateUserAsync(int userId, UpdateUserDto dto);
        Task<PagedResult<User>> GetUsersAsync(int PageNumber, int PageSize);
    }
}
