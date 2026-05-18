using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Repositories;

public interface ISaleRepository
{
    Task<Sale?> GetByIdWithItemsAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Sale?> GetByIdWithItemsTrackedAsync(Guid id, CancellationToken cancellationToken = default);

    Task<Sale> CreateAsync(Sale sale, CancellationToken cancellationToken = default);

    Task UpdateAsync(Sale sale, CancellationToken cancellationToken = default);

    Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default);

    Task<bool> ExistsBySaleNumberAsync(string saleNumber, Guid? excludeSaleId,
        CancellationToken cancellationToken = default);

    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);

    Task<(IReadOnlyList<Sale> Items, int TotalCount)> ListPagedAsync(
        string? saleNumberContains,
        Guid? branchExternalId,
        Guid? customerExternalId,
        bool? isCancelled,
        int pageNumber,
        int pageSize,
        string sortBy,
        bool sortDescending,
        CancellationToken cancellationToken = default);
}
