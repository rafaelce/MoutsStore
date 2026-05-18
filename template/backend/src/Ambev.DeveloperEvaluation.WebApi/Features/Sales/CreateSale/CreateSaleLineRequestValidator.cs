using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;

public class CreateSaleLineRequestValidator : AbstractValidator<CreateSaleLineRequest>
{
    public CreateSaleLineRequestValidator()
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
