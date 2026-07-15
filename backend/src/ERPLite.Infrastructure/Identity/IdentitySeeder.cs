using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ERPLite.Domain.Entities;

using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace ERPLite.Infrastructure.Identity;

public static class IdentitySeeder
{
    public static async Task SeedAsync(
        IServiceProvider serviceProvider)
    {
        var roleManager =
            serviceProvider
                .GetRequiredService<
                    RoleManager<ApplicationRole>>();

        var userManager =
            serviceProvider
                .GetRequiredService<
                    UserManager<ApplicationUser>>();

        await SeedRolesAsync(roleManager);

        await SeedAdminUserAsync(
            userManager);
    }

    private static async Task SeedRolesAsync(
        RoleManager<ApplicationRole> roleManager)
    {
        string[] roles =
        {
            "Admin",
            "User"
        };

        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                await roleManager.CreateAsync(
                    new ApplicationRole
                    {
                        Name = role
                    });
            }
        }
    }

    private static async Task SeedAdminUserAsync(
        UserManager<ApplicationUser> userManager)
    {
        const string email =
            "admin@erplite.com";

        var user =
            await userManager.FindByEmailAsync(
                email);

        if (user is not null)
        {
            return;
        }

        user = new ApplicationUser
        {
            UserName = email,
            Email = email,
            FirstName = "System",
            LastName = "Administrator",
            EmailConfirmed = true,
            IsActive = true
        };

        var result =
            await userManager.CreateAsync(
                user,
                "Admin@123456");

        if (!result.Succeeded)
        {
            throw new Exception(
                string.Join(
                    ", ",
                    result.Errors.Select(
                        x => x.Description)));
        }

        await userManager.AddToRoleAsync(
            user,
            "Admin");
    }
}
