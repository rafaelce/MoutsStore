using Ambev.DeveloperEvaluation.Application.Common.DTOs.Sale;
using Ambev.DeveloperEvaluation.Application.Sales.Validators;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.Queries;

public class GetAllSaleQueryHandler : IRequestHandler<GetAllSaleQuery, GetAllSalesResult>
{
    private readonly ISaleRepository _saleRepository;
    private readonly IMapper _mapper;

    public GetAllSaleQueryHandler(ISaleRepository saleRepository, IMapper mapper)
    {
        _saleRepository = saleRepository;
        _mapper = mapper;
    }

    public async Task<GetAllSalesResult> Handle(GetAllSaleQuery request, CancellationToken cancellationToken)
    {
        var validator = new GetAllSaleQueryValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var (sales, totalCount) = await _saleRepository.ListPagedAsync(
            request.SaleNumberContains,
            request.BranchExternalId,
            request.CustomerExternalId,
            request.IsCancelled,
            request.PageNumber,
            request.PageSize,
            request.SortBy,
            request.SortDescending,
            cancellationToken);

        var items = _mapper.Map<IReadOnlyList<SaleDto>>(sales);

        return new GetAllSalesResult
        {
            Items = items,
            TotalCount = totalCount
        };
    }
}
