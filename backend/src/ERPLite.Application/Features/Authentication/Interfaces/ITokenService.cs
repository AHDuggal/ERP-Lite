using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ERPLite.Domain.Entities;

namespace ERPLite.Application.Features.Authentication.Interfaces;

public interface ITokenService
{
    string GenerateAccessToken(ApplicationUser user, IList<string> roles);

    RefreshToken GenerateRefreshToken();
}
