using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ERPLite.Application.Features.Organizations.DTOs;

namespace ERPLite.Application.Features.Organizations.Interfaces;

public interface IOrganizationService
{
    Task<OrganizationResponse> CreateAsync(
        CreateOrganizationRequest request,
        CancellationToken cancellationToken);

    Task<List<OrganizationResponse>> GetAllAsync(
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
