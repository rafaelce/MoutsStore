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

    public async Task<bool> ExistsBySaleNumberAsync(
        string saleNumber,
        Guid? excludeSaleId,
        CancellationToken cancellationToken = default)
    {
        var query = Context.Sales.AsQueryable().Where(s => s.SaleNumber == saleNumber);
        if (excludeSaleId.HasValue)
            query = query.Where(s => s.Id != excludeSaleId.Value);

        return await query.AnyAsync(cancellationToken);
    }

    public async Task<(IReadOnlyList<Sale> Items, int TotalCount)> ListPagedAsync(
        string? saleNumberContains,
        Guid? branchExternalId,
        Guid? customerExternalId,
        bool? isCancelled,
        int pageNumber,
        int pageSize,
        string sortBy,
        bool sortDescending,
        CancellationToken cancellationToken = default)
    {
        var query = Context.Sales
            .AsNoTracking()
            .Include(s => s.Items)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(saleNumberContains))
        {
            var pattern = saleNumberContains.Trim();
            query = query.Where(s => s.SaleNumber.Contains(pattern));
        }

        if (branchExternalId.HasValue)
            query = query.Where(s => s.BranchExternalId == branchExternalId.Value);

        if (customerExternalId.HasValue)
            query = query.Where(s => s.CustomerExternalId == customerExternalId.Value);

        if (isCancelled.HasValue)
            query = query.Where(s => s.IsCancelled == isCancelled.Value);

        query = ApplySorting(query, sortBy, sortDescending);

        var totalCount = await query.CountAsync(cancellationToken);

        var items = await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        return (items, totalCount);
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

    private static IQueryable<Sale> ApplySorting(IQueryable<Sale> query, string sortBy, bool sortDescending)
    {
        var key = string.IsNullOrWhiteSpace(sortBy)
            ? "saledate"
            : sortBy.Trim().ToLowerInvariant();

        return key switch
        {
            "salenumber" => sortDescending
                ? query.OrderByDescending(s => s.SaleNumber)
                : query.OrderBy(s => s.SaleNumber),
            "totalamount" => sortDescending
                ? query.OrderByDescending(s => s.TotalAmount)
                : query.OrderBy(s => s.TotalAmount),
            "iscancelled" => sortDescending
                ? query.OrderByDescending(s => s.IsCancelled)
                : query.OrderBy(s => s.IsCancelled),
            "saledate" => sortDescending
                ? query.OrderByDescending(s => s.SaleDate)
                : query.OrderBy(s => s.SaleDate),
            _ => sortDescending
                ? query.OrderByDescending(s => s.SaleDate)
                : query.OrderBy(s => s.SaleDate),
        };
    }
}
