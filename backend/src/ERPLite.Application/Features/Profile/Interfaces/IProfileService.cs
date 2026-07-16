using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ERPLite.Application.Features.Profile.DTOs;

namespace ERPLite.Application.Features.Profile.Interfaces;

public interface IProfileService
{
    Task<MyProfileDto> GetMyProfileAsync(
        CancellationToken cancellationToken);

    Task<MyProfileDto> UpdateMyProfileAsync(
        UpdateMyProfileRequest request,
        CancellationToken cancellationToken);

    Task<UploadProfileImageResponse> UploadProfileImageAsync(
    Stream stream,
    string fileName,
    string contentType,
    CancellationToken cancellationToken);
}
