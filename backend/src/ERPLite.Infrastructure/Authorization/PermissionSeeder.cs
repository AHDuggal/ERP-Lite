using ERPLite.Domain.Constants;
using ERPLite.Domain.Entities;
using ERPLite.Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ERPLite.Infrastructure.Authorization;

public static class PermissionSeeder
{
    public static async Task SeedAsync(
    ApplicationDbContext context,
    RoleManager<ApplicationRole> roleManager)
    {

        var permissions = new[]
 {
    // Organization
    new { Name = "View Organization", Code = Permissions.Organization.View },
    new { Name = "Create Organization", Code = Permissions.Organization.Create },
    new { Name = "Update Organization", Code = Permissions.Organization.Update },
    new { Name = "Delete Organization", Code = Permissions.Organization.Delete },

    // User
    new { Name = "View User", Code = Permissions.User.View },
    new { Name = "Create User", Code = Permissions.User.Create },
    new { Name = "Update User", Code = Permissions.User.Update },
    new { Name = "Delete User", Code = Permissions.User.Delete },

    // Role
    new { Name = "View Role", Code = Permissions.Role.View },
    new { Name = "Create Role", Code = Permissions.Role.Create },
    new { Name = "Update Role", Code = Permissions.Role.Update },
    new { Name = "Delete Role", Code = Permissions.Role.Delete },
    new { Name = "Assign Role", Code = Permissions.Role.Assign },
    new { Name = "Remove Role", Code = Permissions.Role.Remove }
};

        foreach (var permission in permissions)
        {
            var exists = await context.Permissions
                .AnyAsync(x => x.Code == permission.Code);

            if (!exists)
            {
                context.Permissions.Add(
                    new Permission(
                        permission.Name,
                        permission.Code));
            }
        }

        await context.SaveChangesAsync();

        var adminRole =
    await roleManager.FindByNameAsync("Admin");

        if (adminRole is null)
        {
            return;
        }

        var permissionIds =
            await context.Permissions
                .Select(x => x.Id)
                .ToListAsync();

        foreach (var permissionId in permissionIds)
        {
            var exists =
                await context.RolePermissions
                    .AnyAsync(x =>
                        x.RoleId == adminRole.Id &&
                        x.PermissionId == permissionId);

            if (!exists)
            {
                context.RolePermissions.Add(
                    new RolePermission(
                        adminRole.Id,
                        permissionId));
            }
        }

        await context.SaveChangesAsync();

    }
}