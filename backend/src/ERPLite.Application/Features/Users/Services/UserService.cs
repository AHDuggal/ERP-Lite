using ERPLite.Application.Common.Exceptions;
using ERPLite.Application.Features.Authentication.Interfaces;
using ERPLite.Application.Features.Users.DTOs;
using ERPLite.Application.Features.Users.Interfaces;    
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ERPLite.Application.Common.Models;
using ERPLite.Application.Common.Extensions;
namespace ERPLite.Application.Features.Users.Services;

public class UserService : IUserService
{
    private readonly IUserIdentityService _identityService;

    public UserService(
        IUserIdentityService identityService)
    {
        _identityService = identityService;
    }

    // methods...

    public async Task<PagedResult<UserDto>> GetAllAsync(
    QueryParameters parameters,
    CancellationToken cancellationToken)
    {
        var result = await _identityService.GetUsersAsync(
            parameters,
            cancellationToken);

        return new PagedResult<UserDto>
        {
            Items = result.Items.Select(x => new UserDto
            {
                Id = x.Id,

                FirstName = x.FirstName,

                LastName = x.LastName,

                Email = x.Email ?? string.Empty,

                UserName = x.UserName,

                IsActive = x.IsActive,

                JobTitle = x.JobTitle,

                Department = x.Department,

                ProfileImageUrl = x.ProfileImageUrl,

                CreatedOnUtc = x.CreatedOnUtc,

                LastLoginOnUtc = x.LastLoginOnUtc
            }),

            PageNumber = result.PageNumber,

            PageSize = result.PageSize,

            TotalRecords = result.TotalRecords,

            TotalPages = result.TotalPages,

            HasPreviousPage = result.HasPreviousPage,

            HasNextPage = result.HasNextPage
        };
    }
    public async Task<UserDto> GetByIdAsync(
    Guid id,
    CancellationToken cancellationToken)
    {
        var user =
            await _identityService.GetUserByIdAsync(id);

        if (user is null)
        {
            throw new NotFoundException(
                "User",
                id);
        }

        return new UserDto
        {
            Id = user.Id,

            FirstName = user.FirstName,

            LastName = user.LastName,

            Email = user.Email ?? string.Empty,

            UserName = user.UserName,

            IsActive = user.IsActive,

            JobTitle = user.JobTitle,

            Department = user.Department,

            ProfileImageUrl = user.ProfileImageUrl,

            CreatedOnUtc = user.CreatedOnUtc,

            LastLoginOnUtc = user.LastLoginOnUtc
        };
    }

    public async Task<UserDto> CreateAsync(
    CreateUserRequest request,
    CancellationToken cancellationToken)
    {
        var user =
            await _identityService.CreateUserAsync(
                request,
                cancellationToken);

        return await GetByIdAsync(
            user.Id,
            cancellationToken);
    }

    public async Task<UserDto> UpdateAsync(
    UpdateUserRequest request,
    CancellationToken cancellationToken)
    {
        var user =
            await _identityService.UpdateUserAsync(
                request,
                cancellationToken);

        return await GetByIdAsync(
            user.Id,
            cancellationToken);
    }

    public async Task DeleteAsync(
    Guid id,
    CancellationToken cancellationToken)
    {
        await _identityService.DeleteUserAsync(
            id,
            cancellationToken);
    }
}
