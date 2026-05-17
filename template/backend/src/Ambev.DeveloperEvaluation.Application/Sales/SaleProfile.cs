using Ambev.DeveloperEvaluation.Application.Common.DTOs.Sale;
using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Sales;

public class SaleProfile : Profile
{
    public SaleProfile()
    {
        CreateMap<SaleItem, SaleItemDto>();
        CreateMap<Sale, SaleDto>();
    }
}
