using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;
using Trader_Backend.Application.DTOs;
using Trader_Backend.Application.Interfaces.Services;
using Trader_Backend.API.Extensions;



namespace Trader_Backend.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    
    [Authorize] // 🔐 إجباري لفك الـ Token وقراءة الـ Claims بأمان
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login-or-register")]
        public async Task<IActionResult> LoginOrRegister([FromBody] RegisterRequestDto request)
        {
            // 1. استخراج الداتا الرسمية من الـ Claims
            var entraId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? User.FindFirst("oid")?.Value;
            var email = User.FindFirst(ClaimTypes.Email)?.Value ?? User.FindFirst("preferred_username")?.Value;
            var fullName = User.FindFirst(ClaimTypes.Name)?.Value ?? "مستخدم جديد";
            var pictureUrl = User.FindFirst("picture")?.Value;

            if (string.IsNullOrEmpty(entraId) || string.IsNullOrEmpty(email))
            {
                return BadRequest(new { Error = "بيانات الهوية غير مكتملة في الـ Claims." });
            }

            // 2. التحقق التلقائي من دور الـ Admin المربوط بسحابة Azure
            var isSystemAdmin = User.IsInRole("Admin") || User.HasClaim(ClaimTypes.Role, "Admin");
            string finalRole = isSystemAdmin ? "Admin" : "User";

            // 3. التجميع والتمرير للـ Service
            var result = await _authService.ExecuteRegistrationOrLoginAsync(
                entraId, email, fullName, pictureUrl, finalRole, request.InvitationCode, request.PhoneNumber);

            return this.ToActionResult(result);
        }
    }
}

