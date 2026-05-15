using Ambev.DeveloperEvaluation.Application.Common.DTOs.Sale;
using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Sales.Queries;

public class GetSaleByIdProfile : Profile
{
    public GetSaleByIdProfile()
    {
        CreateMap<SaleItem, SaleItemDto>();
        CreateMap<Sale, SaleDto>();
    }
}
