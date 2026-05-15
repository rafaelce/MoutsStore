using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ambev.DeveloperEvaluation.ORM.Configurations;

public class SaleConfiguration : IEntityTypeConfiguration<Sale>
{
    public void Configure(EntityTypeBuilder<Sale> builder)
    {
        builder.ToTable("Sales");
        builder.HasKey(s => s.Id);
        builder.Property(s => s.SaleNumber).IsRequired().HasMaxLength(64);
        builder.Property(s => s.SaleDate).IsRequired();
        builder.Property(s => s.CustomerExternalId).IsRequired();
        builder.Property(s => s.CustomerName).IsRequired().HasMaxLength(256);
        builder.Property(s => s.BranchExternalId).IsRequired();
        builder.Property(s => s.BranchName).IsRequired().HasMaxLength(256);
        builder.Property(s => s.TotalAmount).HasPrecision(18, 2).IsRequired();
        builder.Property(s => s.IsCancelled).IsRequired();
        
        builder.HasMany(s => s.Items)
            .WithOne(i => i.Sale)
            .HasForeignKey(i => i.SaleId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
