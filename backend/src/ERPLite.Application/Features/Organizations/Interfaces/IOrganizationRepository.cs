using ERPLite.Application.Common.Models;
using ERPLite.Domain.Entities;

namespace ERPLite.Application.Features.Organizations.Interfaces;

public interface IOrganizationRepository
{
    Task<Organization> AddAsync(
        Organization organization,
        CancellationToken cancellationToken);

    Task<Organization?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken);

    Task<PagedResult<Organization>> GetAllAsync(
        QueryParameters parameters,
        CancellationToken cancellationToken);

    Task SaveChangesAsync(
        CancellationToken cancellationToken);

    Task<bool> ExistsByCodeAsync(
        string code,
        CancellationToken cancellationToken);

    Task<bool> ExistsByCodeAsync(
        string code,
        Guid excludeId,
        CancellationToken cancellationToken);

    Task DeleteAsync(
        Guid id,
        CancellationToken cancellationToken);

    Task UpdateAsync(
        Organization organization,
        CancellationToken cancellationToken);
}