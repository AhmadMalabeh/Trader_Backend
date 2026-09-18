using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Net.Mime;
using System.Threading.Tasks;

namespace Trader_Backend.API.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (DbUpdateException dbEx)
            {
                _logger.LogError(dbEx, "خطأ أثناء تحديث أو إدخال البيانات في قاعدة البيانات.");
                await HandleDatabaseExceptionAsync(context, dbEx);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "استثناء غير معالج (Unhandled Exception) داخل السيرفر.");
                await HandleGenericExceptionAsync(context, ex);
            }
        }

        private static async Task HandleDatabaseExceptionAsync(HttpContext context, DbUpdateException exception)
        {
            context.Response.ContentType = MediaTypeNames.Application.Json;

            // الفحص الذكي لمعرفة رقم الخطأ القادم من SQL Server
            if (exception.InnerException is SqlException sqlException)
            {
                switch (sqlException.Number)
                {
                    case 547: // Foreign Key Violation
                        context.Response.StatusCode = StatusCodes.Status400BadRequest;
                        await context.Response.WriteAsJsonAsync(new
                        {
                            IsSuccess = false,
                            ErrorMessage = "المعرف (ID) الممرر غير موجود في الجداول المرتبطة. خطأ في قيد الربط الخارجي."
                        });
                        return;

                    case 2601: // Unique Index Violation
                    case 2627: // Unique Constraint / Primary Key Violation
                        context.Response.StatusCode = StatusCodes.Status409Conflict;
                        await context.Response.WriteAsJsonAsync(new
                        {
                            IsSuccess = false,
                            ErrorMessage = "فشلت العملية. القيمة التي تحاول إدخالها مكررة وموجودة مسبقاً."
                        });
                        return;
                }
            }

            // خطأ عام في تحديث البيانات إذا لم يطابق الأرقام أعلاه
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            await context.Response.WriteAsJsonAsync(new
            {
                IsSuccess = false,
                ErrorMessage = "حدث خطأ أثناء معالجة البيانات بقاعدة البيانات. تحقق من صحة المدخلات."
            });
        }

        private static async Task HandleGenericExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = MediaTypeNames.Application.Json;
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;

            await context.Response.WriteAsJsonAsync(new
            {
                IsSuccess = false,
                ErrorMessage = "حصل خطأ غير متوقع في السيرفر، يرجى المحاولة لاحقاً."
            });
        }
    }
}
