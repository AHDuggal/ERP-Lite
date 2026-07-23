using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPLite.Application.Features.Authorization.Interfaces;

public interface IPermissionRepository
{
    Task<HashSet<string>> GetPermissionsAsync(
    Guid userId,
    CancellationToken cancellationToken);
}