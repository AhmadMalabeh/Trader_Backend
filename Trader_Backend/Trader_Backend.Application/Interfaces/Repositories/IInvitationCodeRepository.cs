using System;
using System.Collections.Generic;
using System.Text;
using Trader_Backend.Application.Common;
using Trader_Backend.Domain.Entities;

namespace Trader_Backend.Application.Interfaces.Repositories
{
    public interface IInvitationCodeRepository
    {
        Task AddNewInvitationCodeAsync(string code, int CreatedByAdminID);
        Task<bool> DeleteInvitationCodeByIDAsync(InvitationCode invitationCode);
        Task<InvitationCode?> GetInvitationCodeByCodeAsync(string code);
    }
}
