using Ambev.DeveloperEvaluation.Application.Sales.Queries;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.Validators;

public class GetSaleByIdQueryValidator : AbstractValidator<GetSaleByIdQuery>
{
    public GetSaleByIdQueryValidator()
    {
        RuleFor(q => q.Id).NotEmpty();
    }
}
