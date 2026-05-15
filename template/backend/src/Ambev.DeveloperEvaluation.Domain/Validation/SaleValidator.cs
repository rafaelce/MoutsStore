using Ambev.DeveloperEvaluation.Domain.Entities;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Domain.Validation;

public class SaleValidator : AbstractValidator<Sale>
{
    public SaleValidator()
    {
        RuleFor(s => s.SaleNumber).NotEmpty().MaximumLength(64);
        RuleFor(s => s.CustomerExternalId).NotEmpty();
        RuleFor(s => s.CustomerName).NotEmpty().MaximumLength(256);
        RuleFor(s => s.BranchExternalId).NotEmpty();
        RuleFor(s => s.BranchName).NotEmpty().MaximumLength(256);
        RuleFor(s => s.Items).NotEmpty();
        RuleForEach(s => s.Items).SetValidator(new SaleItemValidator());
    }
}
