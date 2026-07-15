using ERPLite.Application.Common.Exceptions;
using ERPLite.Application.Features.Roles.DTOs;
using ERPLite.Application.Features.Roles.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using ERPLite.Domain.Entities;

namespace ERPLite.Application.Features.Roles.Services;

public sealed class RoleService : IRoleService
{
    private readonly IRoleIdentityService _roleIdentityService;

    public RoleService(
        IRoleIdentityService roleIdentityService)
    {
        _roleIdentityService = roleIdentityService;
    }

    public async Task<IEnumerable<RoleDto>> GetAllAsync(
        CancellationToken cancellationToken)
    {
        var roles =
            await _roleIdentityService.GetRolesAsync(
                cancellationToken);

        return roles.Select(x => new RoleDto
        {
            Id = x.Id,
            Name = x.Name ?? string.Empty,
            Description = x.Description
        });
    }

    public async Task<RoleDto?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken)
    {
        var role =
            await _roleIdentityService.GetRoleByIdAsync(id);

        if (role is null)
        {
            return null;
        }

        return new RoleDto
        {
            Id = role.Id,
            Name = role.Name ?? string.Empty,
            Description = role.Description
        };
    }

    public async Task<RoleDto> CreateAsync(
        CreateRoleRequest request,
        CancellationToken cancellationToken)
    {
        var role =
            await _roleIdentityService.CreateRoleAsync(
                request,
                cancellationToken);

        return new RoleDto
        {
            Id = role.Id,
            Name = role.Name ?? string.Empty,
            Description = role.Description
        };
    }

    public async Task<RoleDto> UpdateAsync(
        UpdateRoleRequest request,
        CancellationToken cancellationToken)
    {
        var role =
            await _roleIdentityService.UpdateRoleAsync(
                request,
                cancellationToken);

        return new RoleDto
        {
            Id = role.Id,
            Name = role.Name ?? string.Empty,
            Description = role.Description
        };
    }

    public async Task DeleteAsync(
        Guid id,
        CancellationToken cancellationToken)
    {
        await _roleIdentityService.DeleteRoleAsync(
            id,
            cancellationToken);
    }

    public async Task AssignRoleAsync(
        AssignRoleRequest request,
        CancellationToken cancellationToken)
    {
        await _roleIdentityService.AssignRoleAsync(
            request.UserId,
            request.RoleName,
            cancellationToken);
    }

    public async Task RemoveRoleAsync(
        AssignRoleRequest request,
        CancellationToken cancellationToken)
    {
        await _roleIdentityService.RemoveRoleAsync(
            request.UserId,
            request.RoleName,
            cancellationToken);
    }

    public async Task<IList<string>> GetUserRolesAsync(
        Guid userId,
        CancellationToken cancellationToken)
    {
        return await _roleIdentityService.GetUserRolesAsync(
            userId,
            cancellationToken);
    }
}