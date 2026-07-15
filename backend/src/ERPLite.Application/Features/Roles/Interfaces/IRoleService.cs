using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ERPLite.Application.Features.Roles.DTOs;

namespace ERPLite.Application.Features.Roles.Interfaces;

public interface IRoleService
{
    Task<IEnumerable<RoleDto>> GetAllAsync(
        CancellationToken cancellationToken);

    Task<RoleDto?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken);

    Task<RoleDto> CreateAsync(
        CreateRoleRequest request,
        CancellationToken cancellationToken);

    Task<RoleDto> UpdateAsync(
        UpdateRoleRequest request,
        CancellationToken cancellationToken);

    Task DeleteAsync(
        Guid id,
        CancellationToken cancellationToken);

    Task AssignRoleAsync(
        AssignRoleRequest request,
        CancellationToken cancellationToken);

    Task RemoveRoleAsync(
        AssignRoleRequest request,
        CancellationToken cancellationToken);

    Task<IList<string>> GetUserRolesAsync(
        Guid userId,
        CancellationToken cancellationToken);
}
