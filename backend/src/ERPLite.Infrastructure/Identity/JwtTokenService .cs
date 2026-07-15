using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ERPLite.Application.Features.Authentication.Interfaces;
using ERPLite.Domain.Entities;

namespace ERPLite.Infrastructure.Identity;

//added for JWTTokenService constructor 
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using ERPLite.Application.Common.Settings;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

public class JwtTokenService : ITokenService
{

    private readonly JwtSettings _jwtSettings;

    public JwtTokenService(
        IOptions<JwtSettings> jwtOptions)
    {
        _jwtSettings = jwtOptions.Value;
    }
    public string GenerateAccessToken(
    ApplicationUser user,
    IList<string> roles)
    {        
        var claims = new List<Claim>
    {
        new Claim(
            JwtRegisteredClaimNames.Sub,
            user.Id.ToString()),

         new Claim(
            JwtRegisteredClaimNames.Email,
            user.Email ?? string.Empty),

             new Claim(
            JwtRegisteredClaimNames.UniqueName,
            user.UserName ?? string.Empty)
};

        claims.AddRange(
            roles.Select(role =>
                new Claim(ClaimTypes.Role, role)));

        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(
                _jwtSettings.SecretKey));

        var credentials =
            new SigningCredentials(
                key,
                SecurityAlgorithms.HmacSha256);

        var token =
            new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(
                    _jwtSettings.AccessTokenExpirationMinutes),
                signingCredentials: credentials);

        return new JwtSecurityTokenHandler()
            .WriteToken(token);
    }

    public RefreshToken GenerateRefreshToken()
    {
        var randomBytes = new byte[64];

        using var rng =
            RandomNumberGenerator.Create();

        rng.GetBytes(randomBytes);

        return new RefreshToken
        {
            Token =
                Convert.ToBase64String(
                    randomBytes),

            ExpiresOnUtc =
                DateTime.UtcNow.AddDays(
                    _jwtSettings
                        .RefreshTokenExpirationDays),

            CreatedOnUtc =
                DateTime.UtcNow
        };
    }
}
