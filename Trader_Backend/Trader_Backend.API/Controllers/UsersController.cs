using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Trader_Backend.API.Extensions; // 🚀 تأكد من استدعاء هذا الـ Namespace لتفعيل ToActionResult
using Trader_Backend.Application.Common;
using Trader_Backend.Application.DTOs;
using Trader_Backend.Application.Interfaces.Services;
using Trader_Backend.Domain.Entities;

namespace Trader_Backend.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        // 1. جلب مستخدم بواسطة الـ ID مع حماية الـ Route من الصفر والسوالب
        [HttpGet("{id:int:min(1)}")]
        public async Task<IActionResult> GetById(int id)
        {
            var user = await _userService.GetUserByIdAsync(id);

            if (user == null)
            {
                // نمرر الفشل للـ Extension Method لتعود تلقائياً بـ NotFound 404
                return this.ToActionResult(OperationResult<User>.Failure(ApplicationErrorCode.NotFound, "عذراً، المستخدم غير موجود في النظام."));
            }

            return this.ToActionResult(OperationResult<User>.Success(user));
        }

        // 2. جلب مستخدم بواسطة البريد الإلكتروني (Email)
        [HttpGet("by-email")]
        public async Task<IActionResult> GetByEmail([FromQuery] string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return BadRequest(new { Error = "يرجى إدخال بريد إلكتروني صحيح." });
            }

            var user = await _userService.GetUserByEmailAsync(email);

            if (user == null)
            {
                return this.ToActionResult(OperationResult<User>.Failure(ApplicationErrorCode.NotFound, "عذراً، لا يوجد مستخدم مسجل بهذا البريد الإلكتروني."));
            }

            return this.ToActionResult(OperationResult<User>.Success(user));
        }

        // 3. جلب مستخدم بواسطة الـ Entra ID (Object ID لـ مايكروسوفت أو جوجل)
        [HttpGet("by-entra/{entraId}")]
        public async Task<IActionResult> GetByEntraId(string entraId)
        {
            if (string.IsNullOrEmpty(entraId))
            {
                return BadRequest(new { Error = "معرّف الهوية الخارجي غير صحيح." });
            }

            var user = await _userService.GetUserByEntraIdAsync(entraId);

            if (user == null)
            {
                return this.ToActionResult(OperationResult<User>.Failure(ApplicationErrorCode.NotFound, "عذراً، هذا الحساب غير مربوط بأي مستخدم لدينا."));
            }

            return this.ToActionResult(OperationResult<User>.Success(user));
        }

        // 4. تحديث بيانات المستخدم (باستخدام الـ DTO والتحقق التلقائي من الـ FluentValidation)
        [HttpPut("{id:int:min(1)}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateUserDto dto)
        {
            // استدعاء الميثود المعدلة بالـ enum
            var result = await _userService.UpdateUserAsync(id, dto);

            // سطر واحد سحري يشغل الـ switch ويرجع النتائج للمستخدم بدقة
            return this.ToActionResult(result);
        }

        // 5. جلب قائمة المستخدمين مقسمة لصفحات (Pagination)
        [HttpGet("get-all")]
        public async Task<IActionResult> GetAll([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            // فحص أمان سريع لمنع الأرقام العبثية والسالبة
            if (pageNumber < 1) pageNumber = 1;
            if (pageSize < 1 || pageSize > 50) pageSize = 10;

            // جلب البيانات الـ Paged من الـ Service
            var pagedUsers = await _userService.GetUsersAsync(pageNumber, pageSize);

            // نرجع النجاح ونمرر كائن الـ PagedResult الموحد للـ Frontend
            return this.ToActionResult(OperationResult<PagedResult<User>>.Success(pagedUsers));
        }
    }
}

