using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPLite.Domain.Entities;

public class RefreshToken
{
    public Guid Id { get; set; }

    public string Token { get; set; }
        = string.Empty;

    public DateTime ExpiresOnUtc { get; set; }

    public DateTime CreatedOnUtc { get; set; }
        = DateTime.UtcNow;

    public DateTime? RevokedOnUtc { get; set; }

    public Guid UserId { get; set; }

    public ApplicationUser User { get; set; }
        = null!;


    public string? ReplacedByToken { get; set; }

    public string? RevocationReason { get; set; }

    public bool IsRevoked =>
        RevokedOnUtc.HasValue;

    public bool IsExpired =>
        DateTime.UtcNow >= ExpiresOnUtc;

    public bool IsActive =>
       !IsExpired && !IsRevoked;


}
