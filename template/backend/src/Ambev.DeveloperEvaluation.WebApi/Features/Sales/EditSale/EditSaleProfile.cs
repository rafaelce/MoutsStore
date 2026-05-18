using Ambev.DeveloperEvaluation.Application.Sales.Commands.Edit;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.EditSale;

public class EditSaleProfile : Profile
{
    public EditSaleProfile()
    {
        CreateMap<EditSaleRequest, EditSaleCommand>()
            .ForMember(d => d.Id, o => o.Ignore());
    }
}
