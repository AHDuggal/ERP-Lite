using ERPLite.Domain.Common;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPLite.Domain.Entities;

public sealed class RolePermission : BaseEntity
{
    public Guid RoleId { get; private set; }

    public Guid PermissionId { get; private set; }

    private RolePermission()
    {
    }

    public RolePermission(
        Guid roleId,
        Guid permissionId)
    {
        RoleId = roleId;
        PermissionId = permissionId;
    }

    public Permission Permission { get; private set; } = null!;
}