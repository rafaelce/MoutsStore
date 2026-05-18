using Ambev.DeveloperEvaluation.Application.Common.DTOs.Sale;
using Ambev.DeveloperEvaluation.Application.Sales.Validators;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.Queries;

public class GetSaleByIdQueryHandler : IRequestHandler<GetSaleByIdQuery, SaleDto>
{
    private readonly ISaleRepository _saleRepository;
    private readonly IMapper _mapper;

    public GetSaleByIdQueryHandler(ISaleRepository saleRepository, IMapper mapper)
    {
        _saleRepository = saleRepository;
        _mapper = mapper;
    }

    public async Task<SaleDto> Handle(GetSaleByIdQuery request, CancellationToken cancellationToken)
    {
        var validator = new GetSaleByIdQueryValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var sale = await _saleRepository.GetByIdWithItemsAsync(request.Id, cancellationToken);

        if (sale is null)
            throw new KeyNotFoundException($"Venda com ID {request.Id} não encontrada.");

        return _mapper.Map<SaleDto>(sale);
    }
}
