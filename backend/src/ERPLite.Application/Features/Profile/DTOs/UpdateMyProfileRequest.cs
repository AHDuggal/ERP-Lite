using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPLite.Application.Features.Profile.DTOs;

public sealed class UpdateMyProfileRequest
{
    public string FirstName { get; set; }
        = string.Empty;

    public string LastName { get; set; }
        = string.Empty;

    public string? PhoneNumber { get; set; }

    public string? JobTitle { get; set; }

    public string? Department { get; set; }
}
