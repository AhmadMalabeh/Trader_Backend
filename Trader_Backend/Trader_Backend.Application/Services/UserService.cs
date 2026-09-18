using System;
using System.Collections.Generic;
using System.Text;
using Trader_Backend.Application.Interfaces.Services;
using Trader_Backend.Application.Interfaces.Repositories;
using Trader_Backend.Application.Common;
using Trader_Backend.Application.DTOs;
using Trader_Backend.Domain.Entities;
namespace Trader_Backend.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User?> GetUserByIdAsync(int UserId)
        {

            return await _userRepository.GetUserByIdAsync(UserId);
        }

        public async Task<User?> GetUserByEmailAsync(string Email)
        {

            return await _userRepository.GetUserByEmailAsync(Email);
        }

        public async Task<User?> GetUserByEntraIdAsync(string EntraId)
        {

            return await _userRepository.GetUserByEntraIdAsync(EntraId);
        }

        public async Task<OperationResult<User>> UpdateUserAsync(int userId, UpdateUserDto dto)
        {
            // 1. جلب المستخدم من الـ Repository
            var user = await _userRepository.GetUserByIdAsync(userId);

            // 2. التحقق واستخدام الـ Enum الموحد في حالة الفشل 
            if (user == null)
            {
                // 🎯 نرجع الفشل باستخدام الـ Enum والرسالة العربية بشكل Type-Safe
                return OperationResult<User>.Failure(ApplicationErrorCode.NotFound, "عذراً، المستخدم غير موجود في النظام.");
            }

            // 3. تعديل البيانات إذا وُجد
            user.FullName = dto.FullName;
            user.Email = dto.Email;
            if (!string.IsNullOrEmpty(dto.PhoneNumber)) user.PhoneNumber = dto.PhoneNumber;

            // 4. الحفظ عبر الـ Repository
            await _userRepository.UpdateUserAsync(user);
            await _userRepository.SaveChangesAsync();

            // 🎉 إرجاع كائن النجاح
            return OperationResult<User>.Success(user);
        }

        public async Task<PagedResult<User>> GetUsersAsync(int PageNumber, int PageSize)
        {
            return await _userRepository.GetUsersAsync(PageNumber, PageSize);
        }
    }
}
