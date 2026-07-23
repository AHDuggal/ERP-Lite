using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ERPLite.Application.Features.Authorization.Interfaces;
using ERPLite.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ERPLite.Infrastructure.Repositories;

public sealed class PermissionRepository
    : IPermissionRepository
{
    private readonly ApplicationDbContext _context;

    public PermissionRepository(
        ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<HashSet<string>> GetPermissionsAsync(
    Guid userId,
    CancellationToken cancellationToken)
    {
        var roleIds =
            await _context.UserRoles
                .Where(x => x.UserId == userId)
                .Select(x => x.RoleId)
                .ToListAsync(cancellationToken);

        if (!roleIds.Any())
        {
            return [];
        }

        var permissions =
            await _context.RolePermissions
                .Where(x => roleIds.Contains(x.RoleId))
                .Join(
                    _context.Permissions,
                    rp => rp.PermissionId,
                    p => p.Id,
                    (rp, p) => p.Code)
                .ToListAsync(cancellationToken);

        return permissions.ToHashSet();
    }
}