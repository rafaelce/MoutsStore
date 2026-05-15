using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.ORM.Repositories;

public class SaleRepository : GenericRepository<Sale>, ISaleRepository
{
    public SaleRepository(DefaultContext context) : base(context){}

    public async Task<Sale?> GetByIdWithItemsAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await Context.Sales
            .AsNoTracking()
            .Include(s => s.Items)
            .FirstOrDefaultAsync(s => s.Id == id, cancellationToken);
    }

    public async Task<Sale?> GetByIdWithItemsTrackedAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await Context.Sales
            .Include(s => s.Items)
            .FirstOrDefaultAsync(s => s.Id == id, cancellationToken);
    }

    public Task<Sale> CreateAsync(Sale sale, CancellationToken cancellationToken = default)
    {
        return AddAsync(sale);
    }

    public Task UpdateAsync(Sale sale, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        return base.UpdateAsync(sale);
    }

    public async Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await Context.Sales.AnyAsync(s => s.Id == id, cancellationToken);
    }

    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var sale = await DbSet.FindAsync([id], cancellationToken);
        if (sale is null)
            return false;

        DbSet.Remove(sale);
        await Context.SaveChangesAsync(cancellationToken);
        return true;
    }
}
