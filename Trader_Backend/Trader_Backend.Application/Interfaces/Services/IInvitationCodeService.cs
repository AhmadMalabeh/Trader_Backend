using System;
using System.Collections.Generic;
using System.Text;
using Trader_Backend.Domain.Entities;
using Trader_Backend.Application.Common;
namespace Trader_Backend.Application.Interfaces.Services
{
    public interface IInvitationCodeService
    {
        Task<OperationResult<bool>> AddNewInvitationCodeAsync(string code, int CreatedByAdminID);
        Task<OperationResult<bool>> DeleteInvitationCodeAsync(string Code);
        Task<OperationResult<string>> GenerateInvitationCode();
    }
}
