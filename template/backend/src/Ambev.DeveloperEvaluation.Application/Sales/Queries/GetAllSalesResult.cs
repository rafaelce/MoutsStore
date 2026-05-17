using Ambev.DeveloperEvaluation.Application.Common.DTOs.Sale;

namespace Ambev.DeveloperEvaluation.Application.Sales.Queries;

public sealed class GetAllSalesResult
{
    public IReadOnlyList<SaleDto> Items { get; init; } = Array.Empty<SaleDto>();

    public int TotalCount { get; init; }
}
