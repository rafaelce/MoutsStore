namespace Ambev.DeveloperEvaluation.Application.Common.DTOs.Sale;

public class SaleDto
{
    public Guid Id { get; set; }
    public string SaleNumber { get; set; } = string.Empty;
    public DateTimeOffset SaleDate { get; set; }
    public Guid CustomerExternalId { get; set; }
    public string CustomerName { get; set; } = string.Empty;
    public Guid BranchExternalId { get; set; }
    public string BranchName { get; set; } = string.Empty;
    public decimal TotalAmount { get; set; }
    public bool IsCancelled { get; set; }
    public IReadOnlyList<SaleItemDto> Items { get; set; } = Array.Empty<SaleItemDto>();
}
