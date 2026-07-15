using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Identity;

namespace ERPLite.Domain.Entities;

public class ApplicationUser : IdentityUser<Guid>
{
    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public bool IsActive { get; set; } = true;

    public DateTime CreatedOnUtc { get; set; }
        = DateTime.UtcNow;

    public DateTime? LastLoginOnUtc { get; set; }


    public string? PhoneNumber2 { get; set; }

    public string? JobTitle { get; set; }

    public string? Department { get; set; }

    public string? ProfileImageUrl { get; set; }

    public bool IsDeleted { get; set; }

    public DateTime? UpdatedOnUtc { get; set; }

    public ICollection<RefreshToken> RefreshTokens
        = new List<RefreshToken>();
}
