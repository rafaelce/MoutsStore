using Ambev.DeveloperEvaluation.Application.Sales.Commands.Create;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.Validators;

public class CreateSaleItemCommandValidator : AbstractValidator<CreateSaleItemCommand>
{
    public CreateSaleItemCommandValidator()
    {
        RuleFor(i => i.ProductExternalId)
            .NotEqual(Guid.Empty)
            .WithMessage("O produto é obrigatório.");

        RuleFor(i => i.ProductName)
            .NotEmpty()
            .MaximumLength(256);

        RuleFor(i => i.Quantity)
            .InclusiveBetween(1, 20)
            .WithMessage("A quantidade deve estar entre 1 e 20.");

        RuleFor(i => i.UnitPrice)
            .GreaterThanOrEqualTo(0)
            .WithMessage("O preço unitário não pode ser negativo.");
    }
}
