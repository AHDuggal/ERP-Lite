using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPLite.Application.Features.Profile.DTOs;

public sealed class MyProfileDto
{
    public Guid Id { get; set; }

    public string FirstName { get; set; }
        = string.Empty;

    public string LastName { get; set; }
        = string.Empty;

    public string Email { get; set; }
        = string.Empty;

    public string? UserName { get; set; }

    public string? PhoneNumber { get; set; }

    public string? JobTitle { get; set; }

    public string? Department { get; set; }

    public string? ProfileImageUrl { get; set; }

    public bool IsActive { get; set; }

    public DateTime CreatedOnUtc { get; set; }

    public DateTime? LastLoginOnUtc { get; set; }
}
