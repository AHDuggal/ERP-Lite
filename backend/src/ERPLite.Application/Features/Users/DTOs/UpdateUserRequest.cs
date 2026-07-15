using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPLite.Application.Features.Users.DTOs;

public sealed class UpdateUserRequest
{
    public Guid Id { get; set; }

    public string FirstName { get; set; }
        = string.Empty;

    public string LastName { get; set; }
        = string.Empty;

    public string? JobTitle { get; set; }

    public string? Department { get; set; }

    public string? PhoneNumber { get; set; }

    public bool IsActive { get; set; }
}
