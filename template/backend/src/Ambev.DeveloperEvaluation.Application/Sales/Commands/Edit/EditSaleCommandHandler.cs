using Ambev.DeveloperEvaluation.Application.Common.DTOs.Sale;
using Ambev.DeveloperEvaluation.Application.Sales.Commands.Create;
using Ambev.DeveloperEvaluation.Application.Sales.Validators;
using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Exceptions;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.Commands.Edit;

public class EditSaleCommandHandler : IRequestHandler<EditSaleCommand, SaleDto>
{
    private readonly ISaleRepository _saleRepository;
    private readonly IMapper _mapper;

    public EditSaleCommandHandler(ISaleRepository saleRepository, IMapper mapper)
    {
        _saleRepository = saleRepository;
        _mapper = mapper;
    }

    public async Task<SaleDto> Handle(EditSaleCommand command, CancellationToken cancellationToken)
    {
        var commandValidator = new EditSaleCommandValidator();
        var commandValidation = await commandValidator.ValidateAsync(command, cancellationToken);
        if (!commandValidation.IsValid)
            throw new ValidationException(commandValidation.Errors);

        var sale = await _saleRepository.GetByIdWithItemsTrackedAsync(command.Id, cancellationToken);
        if (sale is null)
            throw new KeyNotFoundException($"Venda com o ID {command.Id} não encontrada.");

        if (sale.IsCancelled)
            throw new InvalidOperationException("Não é possível editar uma venda com status cancelada.");

        if (await _saleRepository.ExistsBySaleNumberAsync(command.SaleNumber, sale.Id, cancellationToken))
            throw new InvalidOperationException($"Já existe uma venda com o número '{command.SaleNumber}'.");

        sale.SaleNumber = command.SaleNumber;
        sale.SaleDate = command.SaleDate == default ? sale.SaleDate : command.SaleDate;
        sale.CustomerExternalId = command.CustomerExternalId;
        sale.CustomerName = command.CustomerName;
        sale.BranchExternalId = command.BranchExternalId;
        sale.BranchName = command.BranchName;

        var newItems = command.Items.Select(line => new SaleItem
        {
            Id = Guid.NewGuid(),
            SaleId = sale.Id,
            ProductExternalId = line.ProductExternalId,
            ProductName = line.ProductName,
            Quantity = line.Quantity,
            UnitPrice = line.UnitPrice
        });

        try
        {
            sale.ReplaceItems(newItems);
        }
        catch (DomainException ex)
        {
            throw new ValidationException(new[]
            {
                new ValidationFailure(nameof(EditSaleCommand.Items), ex.Message)
            });
        }

        var domainValidation = sale.Validate();
        if (!domainValidation.IsValid)
            throw ToValidationException(domainValidation);

        await _saleRepository.UpdateAsync(sale, cancellationToken);

        return _mapper.Map<SaleDto>(sale);
    }

    private static ValidationException ToValidationException(ValidationResultDetail detail)
    {
        var failures = detail.Errors.Select(e =>
            new ValidationFailure(
                "Sale",
                string.IsNullOrWhiteSpace(e.Detail) ? e.Error : e.Detail));

        return new ValidationException(failures);
    }
}
