using Ambev.DeveloperEvaluation.ORM.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.ORM;

public class DefaultContext(DbContextOptions<DefaultContext> options) : BaseDbContext(options)
{
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        ApplyEntityConfigurations(modelBuilder);
    }

    public override void ApplyEntityConfigurations(ModelBuilder modelBuilder)
    {
        // modelBuilder.ApplyConfiguration(new UserConfiguration());
        // modelBuilder.ApplyConfiguration(new SaleConfiguration());
        // modelBuilder.ApplyConfiguration(new SaleItemConfiguration());

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(UserConfiguration).Assembly);
    }
}