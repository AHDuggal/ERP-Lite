using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ERPLite.Application.Features.Authorization.Interfaces;

namespace ERPLite.Application.Features.Authorization.Services;

public sealed class PermissionService
    : IPermissionService
{
    private readonly IPermissionRepository _repository;

    public PermissionService(
        IPermissionRepository repository)
    {
        _repository = repository;
    }

    public async Task<HashSet<string>> GetPermissionsAsync(
     Guid userId,
     CancellationToken cancellationToken)
    {
        return await _repository.GetPermissionsAsync(
            userId,
            cancellationToken);
    }
}