using Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.EditSale;

public class EditSaleRequest
{
    public string SaleNumber { get; set; } = string.Empty;

    public DateTimeOffset SaleDate { get; set; }

    public Guid CustomerExternalId { get; set; }

    public string CustomerName { get; set; } = string.Empty;

    public Guid BranchExternalId { get; set; }

    public string BranchName { get; set; } = string.Empty;

    public IReadOnlyList<CreateSaleLineRequest> Items { get; set; } = Array.Empty<CreateSaleLineRequest>();
}
