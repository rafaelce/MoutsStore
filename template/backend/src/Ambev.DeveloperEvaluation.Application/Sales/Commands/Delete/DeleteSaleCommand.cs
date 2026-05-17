using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.Commands.Delete;

public class DeleteSaleCommand : IRequest
{
    public Guid Id { get; set; }
}
