namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetAllSales;

public class GetAllSalesRequest
{
    public string? SaleNumberContains { get; set; }

    public Guid? BranchExternalId { get; set; }

    public Guid? CustomerExternalId { get; set; }

    public bool? IsCancelled { get; set; }

    public int PageNumber { get; set; } = 1;

    public int PageSize { get; set; } = 10;

    public string SortBy { get; set; } = "saledate";

    public bool SortDescending { get; set; }
}
