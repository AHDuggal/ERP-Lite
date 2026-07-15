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
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Organization name is required.");

        if (string.IsNullOrWhiteSpace(code))
            throw new ArgumentException("Organization code is required.");
        Name = name;
        Code = code;
    }


    public DateTime CreatedOnUtc { get; private set; }
    = DateTime.UtcNow;

    public DateTime? UpdatedOnUtc { get; private set; }

    public DateTime? DeletedOnUtc { get; private set; }

    public bool IsDeleted { get; private set; }

    public void Update(
    string name,
    string code)
    {
        Name = name;
        Code = code;
        UpdatedOnUtc = DateTime.UtcNow;
    }

    public void Delete()
    {
        IsDeleted = true;
        DeletedOnUtc = DateTime.UtcNow;
    }
}