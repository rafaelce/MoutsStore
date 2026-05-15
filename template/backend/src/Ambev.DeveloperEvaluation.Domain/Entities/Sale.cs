using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Exceptions;
using Ambev.DeveloperEvaluation.Domain.Validation;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

public class Sale : BaseEntity
{
    public string SaleNumber { get; set; } = string.Empty;
    public DateTimeOffset SaleDate { get; set; }
    public Guid CustomerExternalId { get; set; }
    public string CustomerName { get; set; } = string.Empty;
    public Guid BranchExternalId { get; set; }
    public string BranchName { get; set; } = string.Empty;
    public decimal TotalAmount { get; private set; }
    public bool IsCancelled { get; private set; }
    public ICollection<SaleItem> Items { get; protected set; } = new List<SaleItem>();

    public ValidationResultDetail Validate()
    {
        var validator = new SaleValidator();
        var result = validator.Validate(this);
        return new ValidationResultDetail
        {
            IsValid = result.IsValid,
            Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
        };
    }

    public void AddItem(SaleItem item)
    {
        if (IsCancelled) throw new DomainException("Venda cancelada não pode ser alterada.");
        item.SaleId = Id;
        item.ApplyQuantityTierPricing();
        Items.Add(item);
        RecalculateTotal();
    }
    public void RecalculateTotal()
    {
        TotalAmount = Items.Where(i => !i.IsCancelled).Sum(i => i.LineTotal);
    }
    public void Cancel()
    {
        if (IsCancelled) return;
        IsCancelled = true;
        foreach (var i in Items) i.IsCancelled = true;
        TotalAmount = 0m;
    }
}
