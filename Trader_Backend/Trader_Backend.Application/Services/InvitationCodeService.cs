using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using Trader_Backend.Application.Common;
using Trader_Backend.Application.Interfaces.Repositories;
using Trader_Backend.Application.Interfaces.Services;
using Trader_Backend.Domain.Entities;
namespace Trader_Backend.Application.Services
{
    public class InvitationCodeService : IInvitationCodeService
    {
        private readonly IInvitationCodeRepository _invitationCodeRepository;

        public InvitationCodeService(IInvitationCodeRepository invitationCodeRepository)
        {
            _invitationCodeRepository = invitationCodeRepository;
        }

        private const string Chars = "ABCDEFGHJKLMNPQRSTUVWXYZ23456789";

        public async Task<OperationResult<bool>> AddNewInvitationCodeAsync(string code, int CreatedByAdminID)
        {

            await _invitationCodeRepository.AddNewInvitationCodeAsync(code, CreatedByAdminID);
            return OperationResult<bool>.Success(true);
        }

        public async Task<OperationResult<bool>> DeleteInvitationCodeAsync(string Code)
        {
            var code = await _invitationCodeRepository.GetInvitationCodeByCodeAsync(Code);
            if(code == null)
            {
                return OperationResult<bool>.Failure(ApplicationErrorCode.NotFound,"Invitation code not found.");
            }

            await _invitationCodeRepository.DeleteInvitationCodeByIDAsync(code);

            return OperationResult<bool>.Success(true);
        }

        public async Task<OperationResult<string>> GenerateInvitationCode()
        {
            int segments = 4;
            int segmentLength = 4;
            var random = RandomNumberGenerator.Create();
            var parts = new List<string>();

            for (int i = 0; i < segments; i++)
            {
                var bytes = new byte[segmentLength];
                random.GetBytes(bytes);

                var sb = new StringBuilder(segmentLength);
                foreach (var b in bytes)
                    sb.Append(Chars[b % Chars.Length]);

                parts.Add(sb.ToString());
            }
            var code = string.Join("-", parts);
            return OperationResult<string>.Success(code);
        }

        
    }
}
