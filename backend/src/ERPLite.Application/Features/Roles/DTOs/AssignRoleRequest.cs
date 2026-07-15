using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPLite.Application.Features.Roles.DTOs;

public sealed class AssignRoleRequest
{
    public Guid UserId { get; set; }

    public string RoleName { get; set; }
        = string.Empty;
}