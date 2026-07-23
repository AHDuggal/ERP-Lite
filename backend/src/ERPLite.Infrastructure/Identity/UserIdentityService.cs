using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ERPLite.Application.Features.Users.Interfaces;
using ERPLite.Domain.Entities;
using ERPLite.Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;

using ERPLite.Application.Common.Exceptions;
using ERPLite.Application.Common.Extensions;
using ERPLite.Application.Common.Models;
using ERPLite.Application.Features.Users.DTOs;
using Microsoft.EntityFrameworkCore;

namespace ERPLite.Infrastructure.Identity;

public sealed class UserIdentityService : IUserIdentityService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ApplicationDbContext _dbContext;

    public UserIdentityService(
        UserManager<ApplicationUser> userManager,
        ApplicationDbContext dbContext)
    {
        _userManager = userManager;
        _dbContext = dbContext;
    }

    public async Task<PagedResult<ApplicationUser>> GetUsersAsync(
  QueryParameters parameters,
  CancellationToken cancellationToken)
    {
        var query = _userManager.Users
            .Where(x => !x.IsDeleted);

        // Search
        if (!string.IsNullOrWhiteSpace(parameters.Search))
        {
            query = query.Where(x =>
                (x.FirstName != null &&
                 x.FirstName.Contains(parameters.Search)) ||

                (x.LastName != null &&
                 x.LastName.Contains(parameters.Search)) ||

                (x.Email != null &&
                 x.Email.Contains(parameters.Search)));
        }

        // Generic Sorting
        query = query.ApplySorting(parameters);

        // Default Sorting
        if (string.IsNullOrWhiteSpace(parameters.SortBy))
        {
            query = query
                .OrderBy(x => x.FirstName)
                .ThenBy(x => x.LastName);
        }

        var totalRecords =
            await query.CountAsync(cancellationToken);

        var users =
            await query
                .ApplyPagination(parameters)
                .ToListAsync(cancellationToken);

        return new PagedResult<ApplicationUser>
        {
            Items = users,

            PageNumber = parameters.PageNumber,

            PageSize = parameters.PageSize,

            TotalRecords = totalRecords,

            TotalPages =
                (int)Math.Ceiling(
                    totalRecords /
                    (double)parameters.PageSize),

            HasPreviousPage =
                parameters.PageNumber > 1,

            HasNextPage =
                parameters.PageNumber <
                (int)Math.Ceiling(
                    totalRecords /
                    (double)parameters.PageSize)
        };
    }

    public async Task<ApplicationUser?> GetUserByIdAsync( Guid id)
    {
        return await _userManager.Users
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<ApplicationUser> CreateUserAsync(CreateUserRequest request,
CancellationToken cancellationToken)
    {
        var user = new ApplicationUser
        {
            UserName = request.Email,
            Email = request.Email,

            FirstName = request.FirstName,
            LastName = request.LastName,

            PhoneNumber = request.PhoneNumber,

            JobTitle = request.JobTitle,
            Department = request.Department,

            CreatedOnUtc = DateTime.UtcNow,
            IsActive = true
        };

        var result =
            await _userManager.CreateAsync(
                user,
                request.Password);

        if (!result.Succeeded)
        {
            throw new ValidationException(
                result.Errors.Select(x => x.Description));
        }

        return user;
    }

    public async Task<ApplicationUser> UpdateUserAsync(UpdateUserRequest request,
        CancellationToken cancellationToken)
    {
        var user =
            await GetUserByIdAsync(request.Id);

        if (user is null)
        {
            throw new NotFoundException(
                "User",
                request.Id);
        }

        user.FirstName = request.FirstName;
        user.LastName = request.LastName;

        user.JobTitle = request.JobTitle;
        user.Department = request.Department;
        user.PhoneNumber = request.PhoneNumber;

        user.IsActive = request.IsActive;
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

    public async Task DeleteUserAsync(Guid id,CancellationToken cancellationToken)
    {
        var user =
            await GetUserByIdAsync(id);

        if (user is null)
        {
            throw new NotFoundException(
                "User",
                id);
        }

        user.IsDeleted = true;
        user.UpdatedOnUtc = DateTime.UtcNow;

        await _userManager.UpdateAsync(user);
    }
}
