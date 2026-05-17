using Ambev.DeveloperEvaluation.Application.Common.DTOs.Sale;
using Ambev.DeveloperEvaluation.Application.Sales.Validators;
using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Exceptions;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.Commands.Create;

public class CreateSaleCommandHandler : IRequestHandler<CreateSaleCommand, SaleDto>
{
    private readonly ISaleRepository _saleRepository;
    private readonly IMapper _mapper;

    public CreateSaleCommandHandler(ISaleRepository saleRepository, IMapper mapper)
    {
        _saleRepository = saleRepository;
        _mapper = mapper;
    }

    public async Task<SaleDto> Handle(CreateSaleCommand command, CancellationToken cancellationToken)
    {
        var commandValidator = new CreateSaleCommandValidator();
        var commandValidation = await commandValidator.ValidateAsync(command, cancellationToken);
        if (!commandValidation.IsValid)
            throw new ValidationException(commandValidation.Errors);

        if (await _saleRepository.ExistsBySaleNumberAsync(command.SaleNumber, null, cancellationToken))
            throw new InvalidOperationException($"Já existe uma venda com o número '{command.SaleNumber}'.");

        var sale = new Sale
        {
            Id = Guid.NewGuid(),
            SaleNumber = command.SaleNumber,
            SaleDate = command.SaleDate == default ? DateTimeOffset.UtcNow : command.SaleDate,
            CustomerExternalId = command.CustomerExternalId,
            CustomerName = command.CustomerName,
            BranchExternalId = command.BranchExternalId,
            BranchName = command.BranchName
        };

        foreach (var line in command.Items)
        {
            var item = new SaleItem
            {
                Id = Guid.NewGuid(),
                ProductExternalId = line.ProductExternalId,
                ProductName = line.ProductName,
                Quantity = line.Quantity,
                UnitPrice = line.UnitPrice
            };

            try
            {
                sale.AddItem(item);
            }
            catch (DomainException ex)
            {
                throw new ValidationException(new[]
                {
                    new ValidationFailure(nameof(CreateSaleCommand.Items), ex.Message)
                });
            }
        }

        var domainValidation = sale.Validate();
        if (!domainValidation.IsValid)
            throw ToValidationException(domainValidation);

        var created = await _saleRepository.CreateAsync(sale, cancellationToken);
        return _mapper.Map<SaleDto>(created);
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
