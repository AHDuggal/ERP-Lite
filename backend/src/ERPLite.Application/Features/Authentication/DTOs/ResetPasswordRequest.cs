using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPLite.Application.Features.Authentication.DTOs
{
    public sealed class ResetPasswordRequest
    {
        public string Email { get; set; }
            = string.Empty;

        public string Token { get; set; }
            = string.Empty;

        public string NewPassword { get; set; }
            = string.Empty;

        public string ConfirmPassword { get; set; }
            = string.Empty;
    }
}
