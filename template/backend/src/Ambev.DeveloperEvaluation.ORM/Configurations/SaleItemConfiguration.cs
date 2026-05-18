using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ambev.DeveloperEvaluation.ORM.Configurations;

public class SaleItemConfiguration : IEntityTypeConfiguration<SaleItem>
{
    public void Configure(EntityTypeBuilder<SaleItem> builder)
    {
        builder.ToTable("SaleItems");
        builder.HasKey(i => i.Id);
        builder.Property(i => i.SaleId).IsRequired();
        builder.Property(i => i.ProductExternalId).IsRequired();
        builder.Property(i => i.ProductName).IsRequired().HasMaxLength(256);
        builder.Property(i => i.Quantity).IsRequired();
        builder.Property(i => i.UnitPrice).HasPrecision(18, 2).IsRequired();
        builder.Property(i => i.DiscountPercent).HasPrecision(5, 2).IsRequired();
        builder.Property(i => i.DiscountAmount).HasPrecision(18, 2).IsRequired();
        builder.Property(i => i.LineTotal).HasPrecision(18, 2).IsRequired();
        builder.Property(i => i.IsCancelled).IsRequired();
        builder.HasIndex(i => i.SaleId);
    }
}
