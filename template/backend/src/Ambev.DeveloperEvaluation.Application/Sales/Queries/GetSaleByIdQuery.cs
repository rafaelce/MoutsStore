using Ambev.DeveloperEvaluation.Application.Common.DTOs.Sale;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.Queries;

public class GetSaleByIdQuery : IRequest<SaleDto>
{
    public Guid Id { get; set; }
}
