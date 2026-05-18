using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ambev.DeveloperEvaluation.Application.Common.DTOs.Sale;
using Ambev.DeveloperEvaluation.Application.Sales.Commands.Create;
using Ambev.DeveloperEvaluation.Application.Sales.Commands.Cancel;
using Ambev.DeveloperEvaluation.Application.Sales.Commands.Delete;
using Ambev.DeveloperEvaluation.Application.Sales.Commands.Edit;
using Ambev.DeveloperEvaluation.Application.Sales.Queries;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.CancelSale;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.DeleteSale;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.EditSale;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetAllSales;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSale;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class SalesController : BaseController
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public SalesController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(typeof(PaginatedResponse<SaleDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetAll([FromQuery] GetAllSalesRequest request, CancellationToken cancellationToken)
    {
        var validator = new GetAllSalesRequestValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);

        var query = _mapper.Map<GetAllSaleQuery>(request);
        var result = await _mediator.Send(query, cancellationToken);

        var totalPages = (int)Math.Ceiling(result.TotalCount / (double)request.PageSize);

        return Ok(new PaginatedResponse<SaleDto>
        {
            Success = true,
            Message = "Vendas recuperadas com sucesso",
            Data = result.Items,
            CurrentPage = request.PageNumber,
            TotalPages = totalPages,
            TotalCount = result.TotalCount
        });
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ApiResponseWithData<SaleDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var request = new GetSaleRequest { Id = id };
        var validator = new GetSaleRequestValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);

        var saleQuery = _mapper.Map<GetSaleByIdQuery>(request.Id);
        var saleDto = await _mediator.Send(saleQuery, cancellationToken);

        return Ok(new ApiResponseWithData<SaleDto>
        {
            Success = true,
            Message = "Venda recuperada com sucesso",
            Data = saleDto
        });
    }

    [HttpPost]
    [ProducesResponseType(typeof(ApiResponseWithData<SaleDto>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreateSaleRequest request, CancellationToken cancellationToken)
    {
        var validator = new CreateSaleRequestValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);

        var command = _mapper.Map<CreateSaleCommand>(request);
        var saleDto = await _mediator.Send(command, cancellationToken);

        return Created(string.Empty, new ApiResponseWithData<SaleDto>
        {
            Success = true,
            Message = "Venda criada com sucesso",
            Data = saleDto
        });
    }

    [HttpPut("{id}")]
    [ProducesResponseType(typeof(ApiResponseWithData<SaleDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] EditSaleRequest request, CancellationToken cancellationToken)
    {
        var validator = new EditSaleRequestValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);

        var command = _mapper.Map<EditSaleCommand>(request);
        command.Id = id;

        var saleDto = await _mediator.Send(command, cancellationToken);

        return Ok(new ApiResponseWithData<SaleDto>
        {
            Success = true,
            Message = "Venda atualizada com sucesso",
            Data = saleDto
        });
    }

    [HttpPost("{id}/cancel")]
    [ProducesResponseType(typeof(ApiResponseWithData<SaleDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Cancel([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var request = new CancelSaleRequest { Id = id };
        var validator = new CancelSaleRequestValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);

        var command = _mapper.Map<CancelSaleCommand>(request.Id);
        var saleDto = await _mediator.Send(command, cancellationToken);

        return Ok(new ApiResponseWithData<SaleDto>
        {
            Success = true,
            Message = "Venda cancelada com sucesso",
            Data = saleDto
        });
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var request = new DeleteSaleRequest { Id = id };
        var validator = new DeleteSaleRequestValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);

        var command = _mapper.Map<DeleteSaleCommand>(request.Id);
        await _mediator.Send(command, cancellationToken);

        return Ok(new ApiResponse
        {
            Success = true,
            Message = "Venda excluída com sucesso"
        });
    }
}
