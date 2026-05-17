using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.Queries;

public class GetAllSaleQuery : IRequest<GetAllSalesResult>
{
    public string? SaleNumberContains { get; init; }

    public Guid? BranchExternalId { get; init; }

    public Guid? CustomerExternalId { get; init; }

    public bool? IsCancelled { get; init; }

    public int PageNumber { get; init; } = 1;

    public int PageSize { get; init; } = 10;

    public string SortBy { get; init; } = "saledate";

    public bool SortDescending { get; init; }
}
