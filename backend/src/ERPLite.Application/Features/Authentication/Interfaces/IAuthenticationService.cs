using ERPLite.Application.Features.Authentication.DTOs;
using ERPLite.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ERPLite.Application.Features.Authentication.Interfaces;

public interface IAuthenticationService
{
    Task<LoginResponse> LoginAsync(
        LoginRequest request,
        CancellationToken cancellationToken);

    Task<LoginResponse> RefreshTokenAsync(
        RefreshTokenRequest request,
        CancellationToken cancellationToken);

    //New methods

    Task LogoutAsync(
    LogoutRequest request,
    CancellationToken cancellationToken);

    Task ChangePasswordAsync(
    ChangePasswordRequest request,
    Guid userId,
    CancellationToken cancellationToken);

    Task<ForgotPasswordResponse> ForgotPasswordAsync(
    ForgotPasswordRequest request,
    CancellationToken cancellationToken);

    Task ResetPasswordAsync(
    ResetPasswordRequest request,
    CancellationToken cancellationToken);

}
