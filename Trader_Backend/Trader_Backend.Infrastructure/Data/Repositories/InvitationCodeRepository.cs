using System;
using System.Collections.Generic;
using System.Text;
using Trader_Backend.Domain.Entities;
using Trader_Backend.Application.Interfaces.Repositories;
using Trader_Backend.Application.Common;
using Microsoft.EntityFrameworkCore;
namespace Trader_Backend.Infrastructure.Data.Repositories
{
    public class InvitationCodeRepository : IInvitationCodeRepository
    {
        private readonly AppDbContext _context;
        public InvitationCodeRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddNewInvitationCodeAsync(string code, int CreatedByAdminID)
        {
            await _context.InvitationCodes.AddAsync(new InvitationCode
            {
                CodeHash = code,
                CreatedByAdminID = CreatedByAdminID,
            });
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteInvitationCodeByIDAsync(InvitationCode invitationCode)
        {
            _context.InvitationCodes.Remove(invitationCode);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<InvitationCode?> GetInvitationCodeByCodeAsync(string code)
        {
            return await _context.InvitationCodes.FirstOrDefaultAsync(ic => ic.CodeHash == code);
        }

    }
}
