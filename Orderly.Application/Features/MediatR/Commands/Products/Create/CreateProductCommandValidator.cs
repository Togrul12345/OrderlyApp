using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orderly.Application.Features.MediatR.Commands.Products.Create
{
    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator()
        {
            RuleFor(p => p.RestaurantId)
                .GreaterThan(0)
                .WithMessage("RestaurantId 0-dan böyük olmalıdır.");

            RuleFor(p => p.CategoryId)
                .GreaterThan(0)
                .WithMessage("CategoryId 0-dan böyük olmalıdır.");

            RuleFor(p => p.Name)
                .NotEmpty()
                .NotNull()
                .WithMessage("Məhsul adı boş ola bilməz.")
                .MaximumLength(150)
                .WithMessage("Məhsul adı ən çox 150 simvoldan ibarət ola bilər.");

            RuleFor(p => p.Price)
                .GreaterThan(0)
                .WithMessage("Məhsulun qiyməti 0-dan böyük olmalıdır.")
                .PrecisionScale(18, 2, true)
                .WithMessage("Qiymət formatı yanlışdır (maksimum 18 rəqəm, 2 mərtəbə onluq ola bilər).");
        }
    }
}
