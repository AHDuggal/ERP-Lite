using ERPLite.Application.Features.Authentication.DTOs;
using ERPLite.Application.Features.Authentication.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPLite.Application.Features.Authentication.Services;

using ERPLite.Application.Common.Exceptions;
using ERPLite.Application.Features.Authentication.DTOs;
using ERPLite.Application.Features.Authentication.Interfaces;

public class AuthenticationService  : IAuthenticationService
{

    private readonly IIdentityService _identityService;
    private readonly ITokenService _tokenService;

    public AuthenticationService(
        IIdentityService identityService,
        ITokenService tokenService)
    {
        _identityService = identityService;
        _tokenService = tokenService;
    }

    public async Task<LoginResponse> LoginAsync(
    LoginRequest request,
    CancellationToken cancellationToken)
    {
        var user =
            await _identityService
                .FindByEmailAsync(
                    request.Email);

        if (user is null)
        {
            throw new UnauthorizedException(
                "Invalid email or password.");
        }

        if (!user.IsActive)
        {
            throw new ForbiddenException(
                "User account is inactive.");
        }

        var passwordValid =
            await _identityService
                .CheckPasswordAsync(
                    user,
                    request.Password);

        if (!passwordValid)
        {
            throw new UnauthorizedException(
                "Invalid email or password.");
        }

        var roles =
            await _identityService
                .GetRolesAsync(user);

        var accessToken =
            _tokenService
                .GenerateAccessToken(
                    user,
                    roles);

        var refreshToken =
            _tokenService
                .GenerateRefreshToken();

        await _identityService
            .SaveRefreshTokenAsync(
                refreshToken,
                user,
                cancellationToken);

        return new LoginResponse
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken.Token,
            ExpiresOnUtc =
                refreshToken.ExpiresOnUtc
        };
    }

    public async Task<LoginResponse> RefreshTokenAsync(
    RefreshTokenRequest request,
    CancellationToken cancellationToken)
    {
        var refreshToken =
            await _identityService
                .GetRefreshTokenAsync(
                    request.RefreshToken,
                    cancellationToken);

        if (refreshToken is null)
        {
            throw new UnauthorizedException(
                "Invalid refresh token.");
        }

        if (!refreshToken.IsActive)
        {
            throw new UnauthorizedException(
                "Refresh token has expired or has been revoked.");
        }

        var user =
            await _identityService
                .FindByIdAsync(
                    refreshToken.UserId);

        if (user is null)
        {
            throw new UnauthorizedException(
                "User not found.");
        }

        if (!user.IsActive)
        {
            throw new ForbiddenException(
                "User account is inactive.");
        }

        var roles =
            await _identityService
                .GetRolesAsync(user);

        var accessToken =
            _tokenService.GenerateAccessToken(
                user,
                roles);

        var newRefreshToken =
            _tokenService.GenerateRefreshToken();

        await _identityService.SaveRefreshTokenAsync(
            newRefreshToken,
            user,
            cancellationToken);

        await _identityService.RevokeRefreshTokenAsync(
            refreshToken,
            newRefreshToken.Token,
            "Refresh token rotated.",
            cancellationToken);

        return new LoginResponse
        {
            AccessToken = accessToken,
            RefreshToken = newRefreshToken.Token,
            ExpiresOnUtc = newRefreshToken.ExpiresOnUtc
        };
    }


    public async Task LogoutAsync(
    LogoutRequest request,
    CancellationToken cancellationToken)
    {
        var refreshToken =
            await _identityService
                .GetRefreshTokenAsync(
                    request.RefreshToken,
                    cancellationToken);

        if (refreshToken is null)
        {
            throw new UnauthorizedException(
                "Invalid refresh token.");
        }

        if (!refreshToken.IsActive)
        {
            throw new UnauthorizedException(
                "Refresh token has already expired or has been revoked.");
        }

        await _identityService
            .RevokeRefreshTokenAsync(
                refreshToken,
                string.Empty,
                "User logged out.",
                cancellationToken);
    }


    public async Task ChangePasswordAsync(
    ChangePasswordRequest request,
    Guid userId,
    CancellationToken cancellationToken)
    {
        var user =
            await _identityService
                .FindByIdAsync(userId);

        if (user is null)
        {
            throw new UnauthorizedException(
                "User not found.");
        }

        if (!user.IsActive)
        {
            throw new ForbiddenException(
                "User account is inactive.");
        }

        await _identityService
            .ChangePasswordAsync(
                user,
                request.CurrentPassword,
                request.NewPassword);
    }

    public async Task<ForgotPasswordResponse> ForgotPasswordAsync(
    ForgotPasswordRequest request,
    CancellationToken cancellationToken)
    {
        var user =
            await _identityService.FindByEmailAsync(
                request.Email);

        if (user is null)
        {
            throw new NotFoundException(
                "User",
                request.Email);
        }

        if (!user.IsActive)
        {
            throw new ForbiddenException(
                "User account is inactive.");
        }

        var token =
            await _identityService.GeneratePasswordResetTokenAsync(
                user);

        return new ForgotPasswordResponse
        {
            ResetToken = token
        };
    }


    public async Task ResetPasswordAsync(
    ResetPasswordRequest request,
    CancellationToken cancellationToken)
    {
        var user =
            await _identityService.FindByEmailAsync(
                request.Email);

        if (user is null)
        {
            throw new NotFoundException(
                "User",
                request.Email);
        }

        if (!user.IsActive)
        {
            throw new ForbiddenException(
                "User account is inactive.");
        }

        await _identityService.ResetPasswordAsync(
            user,
            request.Token,
            request.NewPassword);
    }


}
