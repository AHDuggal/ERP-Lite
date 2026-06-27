using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

    Task<List<Organization>> GetAllAsync(
        CancellationToken cancellationToken);

    Task SaveChangesAsync(
        CancellationToken cancellationToken);

    Task<bool> ExistsByCodeAsync(
    string code,
    CancellationToken cancellationToken);
}
