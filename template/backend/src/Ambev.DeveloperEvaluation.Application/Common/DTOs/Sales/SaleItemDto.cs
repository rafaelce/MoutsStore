namespace Ambev.DeveloperEvaluation.Application.Common.DTOs.Sale;

public class SaleItemDto
{
    public Guid Id { get; set; }
    public Guid ProductExternalId { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal DiscountPercent { get; set; }
    public decimal DiscountAmount { get; set; }
    public decimal LineTotal { get; set; }
    public bool IsCancelled { get; set; }
}
