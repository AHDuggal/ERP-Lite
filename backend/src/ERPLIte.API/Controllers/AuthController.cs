using Asp.Versioning;
using ERPLite.Application.Common.Models;
using ERPLite.Application.Features.Authentication.DTOs;
using ERPLite.Application.Features.Authentication.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ERPLite.API.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthenticationService
        _authenticationService;

    public AuthController(
        IAuthenticationService authenticationService)
    {
        _authenticationService =
            authenticationService;
    }

    [AllowAnonymous]
    [HttpPost("login")]

    [ProducesResponseType(typeof(ApiResponse<LoginResponse>),
    StatusCodes.Status200OK)]

    [ProducesResponseType(typeof(ApiErrorResponse),
    StatusCodes.Status400BadRequest)]

    [ProducesResponseType(typeof(ApiErrorResponse),
    StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Login(
    LoginRequest request,
    CancellationToken cancellationToken)
    {
        var result =
            await _authenticationService
                .LoginAsync(
                    request,
                    cancellationToken);

        return Ok(
            ApiResponse<LoginResponse>.Ok(
                result,
                "Login successful."));
    }

    [AllowAnonymous]
    [HttpPost("refresh")]
    [ProducesResponseType(typeof(ApiResponse<LoginResponse>),
    StatusCodes.Status200OK)]

    [ProducesResponseType(typeof(ApiErrorResponse),
    StatusCodes.Status400BadRequest)]

    [ProducesResponseType(typeof(ApiErrorResponse),
    StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Refresh(
        RefreshTokenRequest request,
        CancellationToken cancellationToken)
    {
        var result =
            await _authenticationService
                .RefreshTokenAsync(
                    request,
                    cancellationToken);

        return Ok(
            ApiResponse<LoginResponse>.Ok(
                result,
                "Access token refreshed successfully."));
    }


    [Authorize]
    [HttpPost("logout")]
    public async Task<IActionResult> Logout(
    LogoutRequest request,
    CancellationToken cancellationToken)
    {
        await _authenticationService
            .LogoutAsync(
                request,
                cancellationToken);

        return Ok(
            ApiResponse<object>.Ok(
                default,
                "Logout successful."));
    }

    [Authorize]
    [HttpPost("change-password")]
    public async Task<IActionResult> ChangePassword(
    ChangePasswordRequest request,
    CancellationToken cancellationToken)
    {
        var userId = Guid.Parse(
            User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)!.Value);

        await _authenticationService.ChangePasswordAsync(
            request,
            userId,
            cancellationToken);

        return Ok(
            ApiResponse<object>.Ok(
                default,
                "Password changed successfully."));
    }

    [AllowAnonymous]
    [HttpPost("forgot-password")]
    public async Task<IActionResult> ForgotPassword(
    ForgotPasswordRequest request,
    CancellationToken cancellationToken)
    {
        var result =
            await _authenticationService
                .ForgotPasswordAsync(
                    request,
                    cancellationToken);

        return Ok(
            ApiResponse<ForgotPasswordResponse>.Ok(
                result,
                "Password reset token generated successfully."));
    }

    [AllowAnonymous]
    [HttpPost("reset-password")]
    public async Task<IActionResult> ResetPassword(
    ResetPasswordRequest request,
    CancellationToken cancellationToken)
    {
        await _authenticationService
            .ResetPasswordAsync(
                request,
                cancellationToken);

        return Ok(
            ApiResponse<object>.Ok(
                default,
                "Password has been reset successfully."));
    }
}
