using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using Trader_Backend.Application.DTOs;
namespace Trader_Backend.Application.Validators.UserValidators
{
    public class RegisterRequestDtoValidator : AbstractValidator<RegisterRequestDto>
    {
        public RegisterRequestDtoValidator()
        {

            // شروط التحقق من رقم الهاتف (تعديل ذكي للشبكات الأردنية)
            RuleFor(x => x.PhoneNumber)
                .NotEmpty().WithMessage("رقم الهاتف مطلوب لإتمام عملية التسجيل.")
                .Matches(@"^07[789]\d{7}$").WithMessage("يرجى إدخال رقم هاتف أردني صحيح ومكون من 10 خانات (مثال: 079XXXXXXX).");
        }
    }
}
