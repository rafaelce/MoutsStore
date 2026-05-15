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
        
    }
}