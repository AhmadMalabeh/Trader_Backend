using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
using Trader_Backend.Application.DTOs;
namespace Trader_Backend.Application.Validators.OptionGroupValidators
{
    public class OptionGroupDtoValidator : AbstractValidator<OptionGroupDto>
    {
        public OptionGroupDtoValidator()
        {
            RuleFor(x => x.Value)
                .NotEmpty().WithMessage("القيمه مطلوبة.")
                .MaximumLength(100).WithMessage("يجب ألا يتجاوز طول القيمة 100 حرف.");
            RuleFor(x => x.CategoryID)
                .NotEmpty().WithMessage("رقم الفئة مطلوب.")
                .GreaterThan(0).WithMessage("لا يمكن أن يكون رقم الفئة أقل من 0.");
        }
    }
}
