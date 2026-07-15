using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPLite.Application.Features.Authentication.DTOs
{
    public sealed class ForgotPasswordResponse
    {
        public string ResetToken { get; set; }
            = string.Empty;
    }
}
