using Ambev.DeveloperEvaluation.Application.Sales.Queries;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetAllSales;

public class GetAllSalesProfile : Profile
{
    public GetAllSalesProfile()
    {
        CreateMap<GetAllSalesRequest, GetAllSaleQuery>();
    }
}
