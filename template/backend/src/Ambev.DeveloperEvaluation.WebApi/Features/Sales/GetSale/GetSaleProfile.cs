using Ambev.DeveloperEvaluation.Application.Sales.Queries;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSale;

public class GetSaleProfile : Profile
{
    public GetSaleProfile()
    {
        CreateMap<Guid, GetSaleByIdQuery>()
            .ConstructUsing(id => new GetSaleByIdQuery { Id = id });
    }
}
