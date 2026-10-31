using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orderly.Application.Features.MediatR.Commands.Products.Update
{
    public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductCommandValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("Məhsul ID-si etibarsızdır.");

            RuleFor(x => x.RestaurantId)
                .GreaterThan(0).WithMessage("Restoran seçilməlidir.");

            RuleFor(x => x.CategoryId)
                .GreaterThan(0).WithMessage("Kateqoriya seçilməlidir.");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Məhsul adı boş ola bilməz.")
                .MaximumLength(150).WithMessage("Məhsul adı ən çox 150 simvol ola bilər.");

            RuleFor(x => x.Price)
                .GreaterThan(0).WithMessage("Qiymət 0-dan böyük olmalıdır.");
        }
    }
}
