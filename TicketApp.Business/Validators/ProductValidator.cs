using FluentValidation;
using TicketApp.Core.Entities;

namespace TicketApp.Business.Validators
{
    public class ProductValidator : AbstractValidator<Product>
    {
        public ProductValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("Product name is required.")
                .MaximumLength(100).WithMessage("Product name cannot exceed 100 characters.");

            RuleFor(p => p.Description)
                .MaximumLength(500).WithMessage("Product description cannot exceed 500 characters.");

            RuleFor(p => p.Price)
                .GreaterThanOrEqualTo(0).WithMessage("Product price must be a non-negative value.");
        }
    }
}