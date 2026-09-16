using System;
using System.Threading.Tasks;
using Trader_Backend.Application.Common;
using Trader_Backend.Application.Interfaces.Repositories;
using Trader_Backend.Application.Interfaces.Services;
using Trader_Backend.Domain.Entities;

namespace Trader_Backend.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;

        public AuthService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<OperationResult<User>> ExecuteRegistrationOrLoginAsync(
            string entraId, string email, string fullName, string? pictureUrl,
            string role, string? invitationCode, string phoneNumber)
        {
            // 1. فحص هل المستخدم مسجل مسبقاً؟ (تسجيل دخول تلقائي)
            var existingUser = await _userRepository.GetUserByEntraIdAsync(entraId);
            if (existingUser != null)
            {
                return OperationResult<User>.Success(existingUser);
            }

            // 2. 👑 إذا كانت الـ Role القادمة من الـ Claims هي Admin -> يتخطى كود الدعوة ويحفظ فوراً!
            if (role == "Admin")
            {
                var adminUser = new User
                {
                    FullName = fullName,
                    EntraID = entraId,
                    Email = email,
                    PhoneNumber = phoneNumber,
                    PersonalPictureURL = pictureUrl,
                    Role = "Admin",
                    RegisteredAt = DateTime.UtcNow
                };

                await _userRepository.AddUserAsync(adminUser);
                await _userRepository.SaveChangesAsync();

                return OperationResult<User>.Success(adminUser);
            }

            // 3. 🛑 إذا كان مستخدم عادي (User)، نجبره على تدقيق كود الدعوة في الـ Database
            if (string.IsNullOrEmpty(invitationCode))
            {
                return OperationResult<User>.Failure(ApplicationErrorCode.InvitationCodeNotFound, "عذراً، التسجيل كمستخدم يتطلب إدخال كود دعوة صالح.");
            }

            var invite = await _userRepository.GetInvitationCodeAsync(invitationCode);

            if (invite == null)
            {
                return OperationResult<User>.Failure(ApplicationErrorCode.InvitationCodeNotFound, "كود الدعوة الذي أدخلته غير صحيح.");
            }

            if (invite.IsUsed)
            {
                return OperationResult<User>.Failure(ApplicationErrorCode.InvitationCodeAlreadyUsed, "عذراً، كود الدعوة هذا تم استخدامه من قبل.");
            }

            // 4. إنشاء الحساب العادي واستهلاك الكود
            var newUser = new User
            {
                FullName = fullName,
                EntraID = entraId,
                Email = email,
                PhoneNumber = phoneNumber,
                PersonalPictureURL = pictureUrl,
                Role = "User",
                RegisteredAt = DateTime.UtcNow
            };

            invite.IsUsed = true;
            invite.UsedAt = DateTime.UtcNow;

            await _userRepository.AddUserAsync(newUser);
            await _userRepository.SaveChangesAsync();

            return OperationResult<User>.Success(newUser);
        }
    }
}

