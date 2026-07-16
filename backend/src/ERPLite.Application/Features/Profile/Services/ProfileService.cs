using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ERPLite.Application.Common.Interfaces;
using ERPLite.Application.Common.Exceptions;
using ERPLite.Application.Features.Authentication.Interfaces;
using ERPLite.Application.Features.Profile.DTOs;
using ERPLite.Application.Features.Profile.Interfaces;
using ERPLite.Application.Common.Interfaces;
using ERPLite.Application.Common.Settings;

namespace ERPLite.Application.Features.Profile.Services;



public sealed class ProfileService : IProfileService
{
    private readonly ICurrentUserService _currentUserService;
    private readonly IIdentityService _identityService;
    private readonly IFileStorageService _fileStorageService;

    public ProfileService(
    ICurrentUserService currentUserService,
    IIdentityService identityService,
    IFileStorageService fileStorageService)
    {
        _currentUserService = currentUserService;
        _identityService = identityService;
        _fileStorageService = fileStorageService;
    }

    public async Task<MyProfileDto> GetMyProfileAsync(
        CancellationToken cancellationToken)
    {
        var user =
            await _identityService.GetUserByIdAsync(
                _currentUserService.UserId);

        if (user is null)
        {
            throw new NotFoundException(
                "User",
                _currentUserService.UserId);
        }

        return new MyProfileDto
        {
            Id = user.Id,

            FirstName = user.FirstName,

            LastName = user.LastName,

            Email = user.Email ?? string.Empty,

            UserName = user.UserName,

            PhoneNumber = user.PhoneNumber,

            JobTitle = user.JobTitle,

            Department = user.Department,

            ProfileImageUrl = user.ProfileImageUrl,

            IsActive = user.IsActive,

            CreatedOnUtc = user.CreatedOnUtc,

            LastLoginOnUtc = user.LastLoginOnUtc
        };
    }

    public async Task<MyProfileDto> UpdateMyProfileAsync(
    UpdateMyProfileRequest request,
    CancellationToken cancellationToken)
    {
        var user =
            await _identityService.UpdateMyProfileAsync(
                _currentUserService.UserId,
                request,
                cancellationToken);

        return new MyProfileDto
        {
            Id = user.Id,

            FirstName = user.FirstName,

            LastName = user.LastName,

            Email = user.Email ?? string.Empty,

            UserName = user.UserName,

            PhoneNumber = user.PhoneNumber,

            JobTitle = user.JobTitle,

            Department = user.Department,

            ProfileImageUrl = user.ProfileImageUrl,

            IsActive = user.IsActive,

            CreatedOnUtc = user.CreatedOnUtc,

            LastLoginOnUtc = user.LastLoginOnUtc
        };
    }


    public async Task<UploadProfileImageResponse> UploadProfileImageAsync(
    Stream stream,
    string fileName,
    string contentType,
    CancellationToken cancellationToken)
    {
        var imageUrl =
            await _fileStorageService.UploadProfileImageAsync(
                stream,
                fileName,
                contentType,                
                cancellationToken);

        await _identityService.UpdateProfileImageAsync(
            _currentUserService.UserId,
            imageUrl,
            cancellationToken);

        return new UploadProfileImageResponse
        {
            ImageUrl = imageUrl
        };
    }


}