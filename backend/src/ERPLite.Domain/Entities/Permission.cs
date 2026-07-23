using ERPLite.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace ERPLite.Domain.Entities;

public sealed class Permission : BaseEntity
{
    public string Name { get; private set; } = string.Empty;

    public string Code { get; private set; } = string.Empty;

    private Permission()
    {
    }

    public Permission(
        string name,
        string code)
    {
        Name = name;
        Code = code;
    }
}