using ERPLite.Application.Common.Models;
using ERPLite.Application.Features.Profile.DTOs;
using ERPLite.Application.Features.Roles.DTOs;
using  ERPLite.Application.Features.Users.DTOs;
using ERPLite.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPLite.Application.Features.Authentication.Interfaces;

public interface IIdentityService
{
    Task<ApplicationUser?> FindByEmailAsync(
        string email);

    Task<bool> CheckPasswordAsync(
        ApplicationUser user,
        string password);

    Task<IList<string>> GetRolesAsync(
        ApplicationUser user);

    Task SaveRefreshTokenAsync(
        RefreshToken refreshToken,
        ApplicationUser user,
        CancellationToken cancellationToken);

    Task<RefreshToken?> GetRefreshTokenAsync(
    string token,
    CancellationToken cancellationToken);

    Task RevokeRefreshTokenAsync(
        RefreshToken refreshToken,
        string replacedByToken,
        string reason,
        CancellationToken cancellationToken);


    //User related methods in Identity contract
    Task<ApplicationUser?> FindByIdAsync(
        Guid userId);            

    Task ChangePasswordAsync(
    ApplicationUser user,
    string currentPassword,
    string newPassword);

    Task<string> GeneratePasswordResetTokenAsync(
    ApplicationUser user);

    Task ResetPasswordAsync(
        ApplicationUser user,
        string token,
        string newPassword);

    Task<ApplicationUser> UpdateMyProfileAsync(
    Guid userId,
    UpdateMyProfileRequest request,
    CancellationToken cancellationToken);


    Task UpdateProfileImageAsync(
    Guid userId,
    string imageUrl,
    CancellationToken cancellationToken);
}
