using Catalago.API.Models;
using FluentValidation;

namespace Catalago.API.Validators
{
    public class ProductValidator : AbstractValidator<Product>
    {
        public ProductValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Nome é obrigatório");
        }
    }
}
