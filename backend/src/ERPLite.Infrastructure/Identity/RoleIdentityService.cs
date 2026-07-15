using ERPLite.Application.Common.Exceptions;
using ERPLite.Application.Features.Roles.DTOs;
using ERPLite.Application.Features.Roles.Interfaces;
using ERPLite.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPLite.Infrastructure.Identity
{
    public class RoleIdentityService : IRoleIdentityService
    {

        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public RoleIdentityService(
            RoleManager<ApplicationRole> roleManager,
            UserManager<ApplicationUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public async Task<IEnumerable<ApplicationRole>> GetRolesAsync(
    CancellationToken cancellationToken)
        {
            return await _roleManager.Roles
                .OrderBy(x => x.Name)
                .ToListAsync(cancellationToken);
        }

        public async Task<ApplicationRole?> GetRoleByIdAsync(
    Guid id)
        {
            return await _roleManager.Roles
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<ApplicationRole> CreateRoleAsync(
    CreateRoleRequest request,
    CancellationToken cancellationToken)
        {
            var role = new ApplicationRole
            {
                Name = request.Name,
                Description = request.Description
            };

            var result = await _roleManager.CreateAsync(role);

            if (!result.Succeeded)
            {
                throw new ValidationException(
                    result.Errors.Select(x => x.Description));
            }

            return role;
        }

        public async Task<ApplicationRole> UpdateRoleAsync(
    UpdateRoleRequest request,
    CancellationToken cancellationToken)
        {
            var role = await GetRoleByIdAsync(request.Id);

            if (role is null)
            {
                throw new NotFoundException(
                    "Role",
                    request.Id);
            }

            role.Name = request.Name;
            role.Description = request.Description;

            var result = await _roleManager.UpdateAsync(role);

            if (!result.Succeeded)
            {
                throw new ValidationException(
                    result.Errors.Select(x => x.Description));
            }

            return role;
        }


        public async Task DeleteRoleAsync(
    Guid id,
    CancellationToken cancellationToken)
        {
            var role = await GetRoleByIdAsync(id);

            if (role is null)
            {
                throw new NotFoundException(
                    "Role",
                    id);
            }

            var usersInRole =
                await _userManager.GetUsersInRoleAsync(role.Name!);

            if (usersInRole.Any())
            {
                throw new BusinessRuleException(
                    "Cannot delete a role that is assigned to users.");
            }

            var result = await _roleManager.DeleteAsync(role);

            if (!result.Succeeded)
            {
                throw new ValidationException(
                    result.Errors.Select(x => x.Description));
            }
        }

        public async Task AssignRoleAsync(
    Guid userId,
    string roleName,
    CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(
                userId.ToString());

            if (user is null)
            {
                throw new NotFoundException(
                    "User",
                    userId);
            }

            var roleExists =
                await _roleManager.RoleExistsAsync(roleName);

            if (!roleExists)
            {
                throw new NotFoundException(
                    "Role",
                    roleName);
            }

            if (await _userManager.IsInRoleAsync(user, roleName))
            {
                return;
            }

            var result =
                await _userManager.AddToRoleAsync(
                    user,
                    roleName);

            if (!result.Succeeded)
            {
                throw new ValidationException(
                    result.Errors.Select(x => x.Description));
            }
        }

        public async Task RemoveRoleAsync(
    Guid userId,
    string roleName,
    CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(
                userId.ToString());

            if (user is null)
            {
                throw new NotFoundException(
                    "User",
                    userId);
            }

            if (!await _userManager.IsInRoleAsync(user, roleName))
            {
                return;
            }

            var result =
                await _userManager.RemoveFromRoleAsync(
                    user,
                    roleName);

            if (!result.Succeeded)
            {
                throw new ValidationException(
                    result.Errors.Select(x => x.Description));
            }
        }

        public async Task<IList<string>> GetUserRolesAsync(
    Guid userId,
    CancellationToken cancellationToken)
        {
            var user =
                await _userManager.FindByIdAsync(
                    userId.ToString());

            if (user is null)
            {
                throw new NotFoundException(
                    "User",
                    userId);
            }

            return await _userManager.GetRolesAsync(user);
        }
    }
}
