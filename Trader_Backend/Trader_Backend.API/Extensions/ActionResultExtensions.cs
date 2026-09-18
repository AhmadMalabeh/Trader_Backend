using Microsoft.AspNetCore.Mvc;
using Trader_Backend.Application.Common;

namespace Trader_Backend.API.Extensions
{
    public static class ActionResultExtensions
    {
        // 🧠 ميثود سحرية ممتدة لتحويل أي OperationResult إلى حزمة رد الـ API تلقائياً بالـ Switch
        public static IActionResult ToActionResult<T>(this ControllerBase controller, OperationResult<T> result)
        {
            if (result.IsSuccess)
            {
                return controller.Ok(new { Message = "تمت العملية بنجاح", Data = result.Data });
            }

            // 🎯 جملة الـ Switch الذكية بناءً على الـ enum الذي اقترحته!
            return result.ErrorCode switch
            {
                ApplicationErrorCode.NotFound => controller.NotFound(new { Error = result.ErrorMessage }),
             
                ApplicationErrorCode.UnauthorizedAction => controller.StatusCode(403, new { Error = result.ErrorMessage }),

                ApplicationErrorCode.BadRequest => controller.BadRequest(new { Error = result.ErrorMessage }),

                ApplicationErrorCode.InvalidInput => controller.BadRequest(new { Error = result.ErrorMessage }),

                // أي خطأ آخر يعتبر BadRequest 400 تلقائياً
                _ => controller.BadRequest(new { Error = result.ErrorMessage })
            };
        }
    }
}
