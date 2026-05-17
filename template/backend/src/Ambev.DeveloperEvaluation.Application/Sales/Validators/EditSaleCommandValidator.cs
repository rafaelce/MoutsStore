using Ambev.DeveloperEvaluation.Application.Sales.Commands.Create;
using Ambev.DeveloperEvaluation.Application.Sales.Commands.Edit;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.Validators;

public class EditSaleCommandValidator : AbstractValidator<EditSaleCommand>
{
    public EditSaleCommandValidator()
    {
        RuleFor(s => s.Id).NotEmpty();
        RuleFor(s => s.SaleNumber).NotEmpty().MaximumLength(64);
        RuleFor(s => s.CustomerExternalId).NotEmpty();
        RuleFor(s => s.CustomerName).NotEmpty().MaximumLength(256);
        RuleFor(s => s.BranchExternalId).NotEmpty();
        RuleFor(s => s.BranchName).NotEmpty().MaximumLength(256);
        RuleFor(s => s.Items).NotEmpty();
        RuleForEach(s => s.Items).SetValidator(new CreateSaleItemCommandValidator());
    }
}
