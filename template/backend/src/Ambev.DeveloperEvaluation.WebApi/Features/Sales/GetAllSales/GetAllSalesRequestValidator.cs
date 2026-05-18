using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetAllSales;

public class GetAllSalesRequestValidator : AbstractValidator<GetAllSalesRequest>
{
    private static readonly HashSet<string> AllowedSortFields =
        ["saledate", "salenumber", "totalamount", "iscancelled"];

    public GetAllSalesRequestValidator()
    {
        RuleFor(q => q.PageNumber)
            .GreaterThanOrEqualTo(1)
            .WithMessage("PageNumber deve ser pelo menos 1.");

        RuleFor(q => q.PageSize)
            .InclusiveBetween(1, 500)
            .WithMessage("PageSize deve estar entre 1 e 500.");

        RuleFor(q => q.SortBy)
            .Must(s => string.IsNullOrWhiteSpace(s) || AllowedSortFields.Contains(s.Trim().ToLowerInvariant()))
            .WithMessage(
                "SortBy deve ser um dos: saledate, salenumber, totalamount, iscancelled (ou omitir para usar saledate).");
    }
}
