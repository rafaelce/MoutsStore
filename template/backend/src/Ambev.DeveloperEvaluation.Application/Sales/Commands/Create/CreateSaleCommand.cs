using Ambev.DeveloperEvaluation.Application.Common.DTOs.Sale;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.Commands.Create;

public class CreateSaleCommand : IRequest<SaleDto>
{
    public string SaleNumber { get; set; } = string.Empty;

    public DateTimeOffset SaleDate { get; set; }

    public Guid CustomerExternalId { get; set; }

    public string CustomerName { get; set; } = string.Empty;

    public Guid BranchExternalId { get; set; }

    public string BranchName { get; set; } = string.Empty;

    public IReadOnlyList<CreateSaleItemCommand> Items { get; set; } = Array.Empty<CreateSaleItemCommand>();
}
