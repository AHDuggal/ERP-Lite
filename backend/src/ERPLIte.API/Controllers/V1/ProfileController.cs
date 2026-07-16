using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Asp.Versioning;
using ERPLite.Application.Common.Models;
using ERPLite.Application.Features.Profile.DTOs;
using ERPLite.Application.Features.Profile.Interfaces;
using Microsoft.AspNetCore.Authorization;
using ERPLite.Application.Common.Exceptions;


namespace ERPLite.API.Controllers.V1;

[ApiController]
[ApiVersion(1)]
[Route("api/v{version:apiVersion}/[controller]")]
[Authorize]
[Produces("application/json")]
public sealed class ProfileController : ControllerBase
{
    private readonly IProfileService _profileService;

    public ProfileController(
        IProfileService profileService)
    {
        _profileService = profileService;
    }

    [HttpGet]
    [ProducesResponseType(
        typeof(ApiResponse<MyProfileDto>),
        StatusCodes.Status200OK)]
    [ProducesResponseType(
        typeof(ApiErrorResponse),
        StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Get(
        CancellationToken cancellationToken)
    {
        var result =
            await _profileService.GetMyProfileAsync(
                cancellationToken);

        return Ok(
            ApiResponse<MyProfileDto>.Ok(
                result,
                "Profile retrieved successfully."));
    }

    [HttpPut]
    [ProducesResponseType(
        typeof(ApiResponse<MyProfileDto>),
        StatusCodes.Status200OK)]
    [ProducesResponseType(
        typeof(ApiErrorResponse),
        StatusCodes.Status400BadRequest)]
    [ProducesResponseType(
        typeof(ApiErrorResponse),
        StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Update(
        UpdateMyProfileRequest request,
        CancellationToken cancellationToken)
    {
        var result =
            await _profileService.UpdateMyProfileAsync(
                request,
                cancellationToken);

        return Ok(
            ApiResponse<MyProfileDto>.Ok(
                result,
                "Profile updated successfully."));
    }


    [HttpPost("upload-image")]
    [ProducesResponseType(
    typeof(ApiResponse<UploadProfileImageResponse>),
    StatusCodes.Status200OK)]
    [ProducesResponseType(
    typeof(ApiErrorResponse),
    StatusCodes.Status400BadRequest)]
    [ProducesResponseType(
    typeof(ApiErrorResponse),
    StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> UploadImage(
    IFormFile file,
    CancellationToken cancellationToken)
    {
        if (file is null || file.Length == 0)
        {
            throw new ValidationException(
                new[] { "Please select an image." });
        }

        using var stream = file.OpenReadStream();

        var result =
            await _profileService.UploadProfileImageAsync(
                stream,
                file.FileName,
                file.ContentType,
                cancellationToken);

        return Ok(
            ApiResponse<UploadProfileImageResponse>.Ok(
                result,
                "Profile image uploaded successfully."));
    }
}
