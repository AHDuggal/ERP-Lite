using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPLite.Application.Features.Users.DTOs;

public sealed class UserDto
{
    public Guid Id { get; set; }

    public string FirstName { get; set; }
        = string.Empty;

    public string LastName { get; set; }
        = string.Empty;

    public string Email { get; set; }
        = string.Empty;

    public string? UserName { get; set; }

    public bool IsActive { get; set; }

    public string? JobTitle { get; set; }

    public string? Department { get; set; }

    public string? ProfileImageUrl { get; set; }

    public DateTime CreatedOnUtc { get; set; }

    public DateTime? LastLoginOnUtc { get; set; }

    public IList<string> Roles { get; set; } = new List<string>();
}
