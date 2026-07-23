using ERPLite.Application.Common.Exceptions;
using ERPLite.Application.Common.Extensions;
using ERPLite.Application.Common.Models;
using ERPLite.Application.Features.Authentication.Interfaces;
using ERPLite.Application.Features.Profile.DTOs;
using ERPLite.Application.Features.Users.DTOs;
using ERPLite.Application.Features.Users.Interfaces;
using ERPLite.Domain.Entities;
using ERPLite.Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPLite.Infrastructure.Identity
{
    public class IdentityService : IIdentityService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _dbContext;
        private readonly IUserIdentityService _userIdentityService;

        public IdentityService(
            UserManager<ApplicationUser> userManager, IUserIdentityService userIdentityService,
            ApplicationDbContext dbContext)
        {
            _userManager = userManager;
            _dbContext = dbContext;
            _userIdentityService = userIdentityService;
        }


        public async Task<ApplicationUser?> FindByEmailAsync(string email)
        {
            return await _userManager
                .FindByEmailAsync(email);
        }

        public async Task<bool> CheckPasswordAsync(ApplicationUser user, string password)
        {
            return await _userManager
                .CheckPasswordAsync(
                    user,
                    password);
        }

        public async Task<IList<string>> GetRolesAsync(ApplicationUser user)
        {
            return await _userManager
                .GetRolesAsync(user);
        }

        public async Task SaveRefreshTokenAsync(RefreshToken refreshToken, ApplicationUser user, CancellationToken cancellationToken)

        {
            refreshToken.UserId = user.Id;

            _dbContext.RefreshTokens.Add(
                refreshToken);

            await _dbContext.SaveChangesAsync(
                cancellationToken);
        }

        public async Task<ApplicationUser?> FindByIdAsync(
    Guid userId)
        {
            return await _userManager.FindByIdAsync(
                userId.ToString());
        }

        public async Task<RefreshToken?> GetRefreshTokenAsync(
    string token,
    CancellationToken cancellationToken)
        {
            return await _dbContext.RefreshTokens
                .Include(x => x.User)
                .FirstOrDefaultAsync(
                    x => x.Token == token,
                    cancellationToken);
        }

        public async Task RevokeRefreshTokenAsync(
    RefreshToken refreshToken,
    string replacedByToken,
    string reason,
    CancellationToken cancellationToken)
        {
            refreshToken.RevokedOnUtc =
                DateTime.UtcNow;

            refreshToken.ReplacedByToken =
                replacedByToken;

            refreshToken.RevocationReason =
                reason;

            await _dbContext.SaveChangesAsync(
                cancellationToken);
        }           

        public async Task ChangePasswordAsync(
    ApplicationUser user,
    string currentPassword,
    string newPassword)
        {
            var result =
                await _userManager.ChangePasswordAsync(
                    user,
                    currentPassword,
                    newPassword);

            if (!result.Succeeded)
            {
                throw new ValidationException(
                    result.Errors.Select(x => x.Description));
            }
        }

        public async Task<string> GeneratePasswordResetTokenAsync(
    ApplicationUser user)
        {
            return await _userManager
                .GeneratePasswordResetTokenAsync(user);
        }


        public async Task ResetPasswordAsync(
    ApplicationUser user,
    string token,
    string newPassword)
        {
            var result =
                await _userManager.ResetPasswordAsync(
                    user,
                    token,
                    newPassword);

            if (!result.Succeeded)
            {
                throw new ValidationException(
                    result.Errors
                        .Select(x => x.Description));
            }
        }




        public async Task<ApplicationUser> UpdateMyProfileAsync(
        Guid userId,
        UpdateMyProfileRequest request,
        CancellationToken cancellationToken)
        {
            var user =
                await _userIdentityService.GetUserByIdAsync(userId);

            if (user is null)
            {
                throw new NotFoundException(
                    "User",
                    userId);
            }

            user.FirstName = request.FirstName;

            user.LastName = request.LastName;

            user.PhoneNumber = request.PhoneNumber;

            user.JobTitle = request.JobTitle;

            user.Department = request.Department;

            user.UpdatedOnUtc = DateTime.UtcNow;

            var result =
                await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
            {
                throw new ValidationException(
                    result.Errors.Select(x => x.Description));
            }

            return user;
        }
    

    public async Task UpdateProfileImageAsync(
    Guid userId,
    string imageUrl,
    CancellationToken cancellationToken)
        {
            var user =
                await _userIdentityService.GetUserByIdAsync(userId);

            if (user is null)
            {
                throw new NotFoundException(
                    "User",
                    userId);
            }

            user.ProfileImageUrl = imageUrl;

            user.UpdatedOnUtc = DateTime.UtcNow;

            var result =
                await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
            {
                throw new ValidationException(
                    result.Errors.Select(x => x.Description));
            }
        }


    }
}
