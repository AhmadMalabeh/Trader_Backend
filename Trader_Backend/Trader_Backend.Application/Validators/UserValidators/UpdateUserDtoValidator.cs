using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using Trader_Backend.Application.DTOs;

namespace Trader_Backend.Application.Validators.UserValidators
{
    public class UpdateUserDtoValidator : AbstractValidator<UpdateUserDto>
    {
        public UpdateUserDtoValidator()
        {
            // 1. التحقق من الاسم الكامل
            RuleFor(x => x.FullName)
                .NotEmpty().WithMessage("الاسم الكامل مطلوب ولا يمكن تركه فارغاً.")
                .MaximumLength(300).WithMessage("الاسم الكامل طويل جداً، الحد الأقصى 300 حرف.");

            // 2. التحقق من البريد الإلكتروني
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("البريد الإلكتروني مطلوب.")
                .EmailAddress().WithMessage("يرجى إدخال بريد إلكتروني بصيغة صحيحة (مثال: user@example.com).")
                .MaximumLength(150).WithMessage("البريد الإلكتروني طويل جداً.");

            // 3. التحقق من رقم الهاتف (اختياري: يفحص فقط إذا قام المستخدم بكتابته)
            RuleFor(x => x.PhoneNumber)
                .Matches(@"^07[789]\d{7}$")
                .WithMessage("يرجى إدخال رقم هاتف أردني صحيح مكون من 10 خانات (079, 078, 077).")
                .When(x => !string.IsNullOrEmpty(x.PhoneNumber)); // 👈 يطبق الشرط فقط "عندما" لا يكون الهاتف فارغاً
        }
    }
}
