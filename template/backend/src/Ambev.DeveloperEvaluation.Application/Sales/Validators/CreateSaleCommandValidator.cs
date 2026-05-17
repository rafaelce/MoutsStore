using Ambev.DeveloperEvaluation.Application.Sales.Commands.Create;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.Validators;

public class CreateSaleCommandValidator : AbstractValidator<CreateSaleCommand>
{
    public CreateSaleCommandValidator()
    {
        RuleFor(s => s.SaleNumber).NotEmpty().MaximumLength(64);
        RuleFor(s => s.CustomerExternalId).NotEmpty();
        RuleFor(s => s.CustomerName).NotEmpty().MaximumLength(256);
        RuleFor(s => s.BranchExternalId).NotEmpty();
        RuleFor(s => s.BranchName).NotEmpty().MaximumLength(256);
        RuleFor(s => s.Items).NotEmpty();
        RuleForEach(s => s.Items).SetValidator(new CreateSaleItemCommandValidator());
    }
}
