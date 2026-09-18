using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
using Trader_Backend.Application.DTOs;
namespace Trader_Backend.Application.Validators.CategoryValidators
{
    public class CategoryDtoValidator : AbstractValidator<CategoryDto>
    {
        public CategoryDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("اسم الفئة مطلوب.")
                .MaximumLength(100).WithMessage("اسم الفئة طويل جداً، الحد الأقصى 100 حرف.");
            RuleFor(x => x.Description)
                .MaximumLength(1000).WithMessage("الوصف طويل جداً، الحد الأقصى 1000 حرف.")
                .When(x => !string.IsNullOrEmpty(x.Description)); // يطبق الشرط فقط إذا كان الوصف غير فارغ
        }
    }
}
