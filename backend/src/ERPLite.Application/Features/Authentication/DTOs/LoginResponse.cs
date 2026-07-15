using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPLite.Application.Features.Authentication.DTOs;

public sealed class LoginResponse
{
    public string AccessToken { get; set; }
        = string.Empty;

    public string RefreshToken { get; set; }
        = string.Empty;

    public DateTime ExpiresOnUtc { get; set; }
}