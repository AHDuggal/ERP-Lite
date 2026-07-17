using ERPLite.Application.Common.Exceptions;
using ERPLite.Application.Common.Models;
using ERPLite.Application.Features.Organizations.DTOs;
using ERPLite.Application.Features.Organizations.Interfaces;
using ERPLite.Domain.Entities;
using ValidationException = ERPLite.Application.Common.Exceptions.ValidationException;

namespace ERPLite.Application.Features.Organizations.Services;

public sealed class OrganizationService
    : IOrganizationService
{
    private readonly IOrganizationRepository _repository;

    public OrganizationService(
        IOrganizationRepository repository)
    {
        _repository = repository;
    }

    public async Task<OrganizationResponse> CreateAsync(
        CreateOrganizationRequest request,
        CancellationToken cancellationToken)
    {
        var exists = await _repository.ExistsByCodeAsync(
            request.Code,
            cancellationToken);

        if (exists)
        {
            throw new ValidationException(
                $"Organization code '{request.Code}' already exists.");
        }

        var organization = new Organization(
            request.Name,
            request.Code);

        await _repository.AddAsync(
            organization,
            cancellationToken);

        await _repository.SaveChangesAsync(
            cancellationToken);

        return new OrganizationResponse
        {
            Id = organization.Id,
            Name = organization.Name,
            Code = organization.Code
        };
    }

    public async Task<PagedResult<OrganizationResponse>> GetAllAsync(
        QueryParameters parameters,
        CancellationToken cancellationToken)
    {
        var pagedOrganizations =
            await _repository.GetAllAsync(
                parameters,
                cancellationToken);

        return new PagedResult<OrganizationResponse>
        {
            Items = pagedOrganizations.Items
                .Select(x => new OrganizationResponse
                {
                    Id = x.Id,
                    Name = x.Name,
                    Code = x.Code
                })
                .ToList(),

            PageNumber = pagedOrganizations.PageNumber,
            PageSize = pagedOrganizations.PageSize,
            TotalRecords = pagedOrganizations.TotalRecords,
            TotalPages = pagedOrganizations.TotalPages,
            HasNextPage = pagedOrganizations.HasNextPage,
            HasPreviousPage = pagedOrganizations.HasPreviousPage
        };
    }

    public async Task<OrganizationResponse> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken)
    {
        var organization =
            await _repository.GetByIdAsync(
                id,
                cancellationToken);

        if (organization is null)
        {
            throw new NotFoundException(
                "Organization",
                id.ToString());
        }

        return new OrganizationResponse
        {
            Id = organization.Id,
            Name = organization.Name,
            Code = organization.Code
        };
    }

    public async Task DeleteAsync(
        Guid id,
        CancellationToken cancellationToken)
    {
        await _repository.DeleteAsync(
            id,
            cancellationToken);
    }

    public async Task<OrganizationResponse> UpdateAsync(
        UpdateOrganizationRequest request,
        CancellationToken cancellationToken)
    {
        var organization =
            await _repository.GetByIdAsync(
                request.Id,
                cancellationToken);

        if (organization is null)
        {
            throw new NotFoundException(
                "Organization",
                request.Id);
        }

        var exists =
            await _repository.ExistsByCodeAsync(
                request.Code,
                request.Id,
                cancellationToken);

        if (exists)
        {
            throw new ValidationException(
                $"Organization code '{request.Code}' already exists.");
        }

        organization.Update(
            request.Name,
            request.Code);

        await _repository.UpdateAsync(
            organization,
            cancellationToken);

        return new OrganizationResponse
        {
            Id = organization.Id,
            Name = organization.Name,
            Code = organization.Code
        };
    }
}