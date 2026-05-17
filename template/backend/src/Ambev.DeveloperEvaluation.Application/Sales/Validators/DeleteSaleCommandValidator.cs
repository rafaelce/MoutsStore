using Ambev.DeveloperEvaluation.Application.Sales.Commands.Delete;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.Validators;

public class DeleteSaleCommandValidator : AbstractValidator<DeleteSaleCommand>
{
    public DeleteSaleCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
    }
}
