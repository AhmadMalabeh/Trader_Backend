using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Trader_Backend.API.Extensions;
using Trader_Backend.Application.Common;
using Trader_Backend.Application.Interfaces.Services;
using Trader_Backend.Domain.Entities;
namespace Trader_Backend.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class InvitationCodeController : ControllerBase
    {
        private readonly IInvitationCodeService _invitationCodeService;
        private readonly IUserService _userService;

        public InvitationCodeController(IInvitationCodeService invitationCodeService ,IUserService userService)
        {
            _invitationCodeService = invitationCodeService;
            _userService = userService;
        }

        [HttpGet("generate-invitation-code")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<string>> GenerateInvitationCode()
        {
            var result = await _invitationCodeService.GenerateInvitationCode();
            return Ok(result.Data);
        }


        [HttpPost("add-invitation-code")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddInvitationCode([FromQuery] string code)
        {
            if(string.IsNullOrWhiteSpace(code))
            {
                return this.ToActionResult(OperationResult<bool>.Failure(ApplicationErrorCode.BadRequest, "عذراً، يجب إدخال كود الدعوة."));
            }

            var entraId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? User.FindFirst("oid")?.Value;
            if(entraId == null)
            {
                return this.ToActionResult(OperationResult<User>.Failure(ApplicationErrorCode.NotFound, "عذراً، هذا الحساب غير مربوط بأي مستخدم لدينا."));
            }

            var user = await _userService.GetUserByEntraIdAsync(entraId);

            if(user == null)
            {
                return this.ToActionResult(OperationResult<User>.Failure(ApplicationErrorCode.NotFound, "عذراً، هذا الحساب غير مربوط بأي مستخدم لدينا."));
            }

            var result = await _invitationCodeService.AddNewInvitationCodeAsync(code, user.ID);
            return this.ToActionResult(OperationResult<bool>.Success(result.Data));
        }

        [HttpDelete("delete-invitation-code")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteInvitationCode([FromQuery] string code)
        {
            if(string.IsNullOrWhiteSpace(code))
            {
                return this.ToActionResult(OperationResult<bool>.Failure(ApplicationErrorCode.NotFound, "عذراً، يجب إدخال كود الدعوة."));
            }
            var result = await _invitationCodeService.DeleteInvitationCodeAsync(code);
            return this.ToActionResult(result);
        }
    }
}
