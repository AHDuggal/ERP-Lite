using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ERPLite.Domain.Common;

namespace ERPLite.Domain.Entities;

public sealed class Organization : AuditableEntity
{
    public string Name { get; private set; } = string.Empty;

    public string Code { get; private set; } = string.Empty;

    private Organization()
    {
    }

    public Organization(
        string name,
        string code)
    {
        Name = name;
        Code = code;
    }
}