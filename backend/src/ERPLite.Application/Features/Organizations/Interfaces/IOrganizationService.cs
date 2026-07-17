using ERPLite.Application.Common.Models;
using ERPLite.Application.Features.Organizations.DTOs;

namespace ERPLite.Application.Features.Organizations.Interfaces;

public interface IOrganizationService
{
    Task<OrganizationResponse> CreateAsync(
        CreateOrganizationRequest request,
        CancellationToken cancellationToken);

    Task<PagedResult<OrganizationResponse>> GetAllAsync(
        QueryParameters parameters,
        CancellationToken cancellationToken);

    Task<OrganizationResponse> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken);

    Task DeleteAsync(
        Guid id,
        CancellationToken cancellationToken);

    Task<OrganizationResponse> UpdateAsync(
        UpdateOrganizationRequest request,
        CancellationToken cancellationToken);
}