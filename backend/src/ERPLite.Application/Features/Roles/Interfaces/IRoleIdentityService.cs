using ERPLite.Application.Features.Roles.DTOs;
using ERPLite.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPLite.Application.Features.Roles.Interfaces
{
    public interface IRoleIdentityService
    {
        Task<IEnumerable<ApplicationRole>> GetRolesAsync(
    CancellationToken cancellationToken);

        Task<ApplicationRole?> GetRoleByIdAsync(
            Guid id);

        Task<ApplicationRole> CreateRoleAsync(
            CreateRoleRequest request,
            CancellationToken cancellationToken);

        Task<ApplicationRole> UpdateRoleAsync(
            UpdateRoleRequest request,
            CancellationToken cancellationToken);

        Task DeleteRoleAsync(
            Guid id,
            CancellationToken cancellationToken);

        Task AssignRoleAsync(
            Guid userId,
            string roleName,
            CancellationToken cancellationToken);

        Task RemoveRoleAsync(
            Guid userId,
            string roleName,
            CancellationToken cancellationToken);

        Task<IList<string>> GetUserRolesAsync(
            Guid userId,
            CancellationToken cancellationToken);
    }
}
