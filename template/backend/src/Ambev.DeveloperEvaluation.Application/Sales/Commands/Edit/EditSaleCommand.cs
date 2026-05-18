using Ambev.DeveloperEvaluation.Application.Common.DTOs.Sale;
using Ambev.DeveloperEvaluation.Application.Sales.Commands.Create;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.Commands.Edit;

public class EditSaleCommand : IRequest<SaleDto>
{
    public Guid Id { get; set; }

    public string SaleNumber { get; set; } = string.Empty;

    public DateTimeOffset SaleDate { get; set; }

    public Guid CustomerExternalId { get; set; }

    public string CustomerName { get; set; } = string.Empty;

    public Guid BranchExternalId { get; set; }

    public string BranchName { get; set; } = string.Empty;

    public IReadOnlyList<CreateSaleItemCommand> Items { get; set; } = Array.Empty<CreateSaleItemCommand>();
}
