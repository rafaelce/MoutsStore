using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Exceptions;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

public class SaleItem : BaseEntity
{
    public Guid SaleId { get; set; }
    public Sale Sale { get; set; } = null!;
    public Guid ProductExternalId { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal DiscountPercent { get; private set; }
    public decimal DiscountAmount { get; private set; }
    public decimal LineTotal { get; private set; }
    public bool IsCancelled { get; set; }

    public void ApplyQuantityTierPricing()
    {
        if (Quantity < 1)
            throw new DomainException("A quantidade deve ser pelo menos 1.");
            
        if (Quantity > 20)
            throw new DomainException("Não é possível vender mais de 20 itens idênticos na mesma linha.");
        DiscountPercent = Quantity switch
        {
            < 4 => 0m,
            >= 10 and <= 20 => 20m,
            >= 4 and <= 9 => 10m,
            _ => throw new DomainException("Quantidade inválida.")
        };
        var gross = UnitPrice * Quantity;
        DiscountAmount = Math.Round(gross * DiscountPercent / 100m, 2, MidpointRounding.AwayFromZero);
        LineTotal = Math.Round(gross - DiscountAmount, 2, MidpointRounding.AwayFromZero);
    }

}
