using System;
using System.Collections.Generic;
using System.Text;
using Trader_Backend.Application.Common;
using Trader_Backend.Domain.Entities;

namespace Trader_Backend.Application.Interfaces.Services
{
    public interface IAuthService
    {
        Task<OperationResult<User>> ExecuteRegistrationOrLoginAsync(
            string entraId, string email, string fullName, string? pictureUrl,
            string role, string? invitationCode, string phoneNumber);
    }
}
