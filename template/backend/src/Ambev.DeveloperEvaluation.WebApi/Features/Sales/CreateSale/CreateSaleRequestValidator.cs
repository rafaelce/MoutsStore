using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;

public class CreateSaleRequestValidator : AbstractValidator<CreateSaleRequest>
{
    public CreateSaleRequestValidator()
    {
        RuleFor(s => s.SaleNumber).NotEmpty().MaximumLength(64);
        RuleFor(s => s.CustomerExternalId).NotEmpty();
        RuleFor(s => s.CustomerName).NotEmpty().MaximumLength(256);
        RuleFor(s => s.BranchExternalId).NotEmpty();
        RuleFor(s => s.BranchName).NotEmpty().MaximumLength(256);
        RuleFor(s => s.Items).NotEmpty();
        RuleForEach(s => s.Items).SetValidator(new CreateSaleLineRequestValidator());
    }
}
