using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPLite.Application.Features.Authorization.Interfaces;

public interface IPermissionService
{
    Task<HashSet<string>> GetPermissionsAsync(
    Guid userId,
    CancellationToken cancellationToken);
}
