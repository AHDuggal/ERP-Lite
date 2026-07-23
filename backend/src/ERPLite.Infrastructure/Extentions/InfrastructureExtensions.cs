using ERPLite.Application.Common.Interfaces;
using ERPLite.Application.Common.Settings;
using ERPLite.Application.Features.Authentication.Interfaces;
using ERPLite.Application.Features.Authorization.Interfaces;
using ERPLite.Application.Features.Organizations.Interfaces;
using ERPLite.Application.Features.Roles.Interfaces;
using ERPLite.Application.Features.Users.Interfaces;
using ERPLite.Domain.Entities;
using ERPLite.Infrastructure.Identity;
using ERPLite.Infrastructure.Persistence;
using ERPLite.Infrastructure.Repositories;
using ERPLite.Infrastructure.Storage;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;





namespace ERPLite.Infrastructure.Extensions;

public static class InfrastructureExtensions
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services, IConfiguration configuration)
    {

        services.Configure<AzureBlobStorageSettings>(
    configuration.GetSection(
        AzureBlobStorageSettings.SectionName));

        services.AddConfiguration(configuration);
        services.AddAuthenticationConfiguration(configuration);
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        
        services.AddDbContext<ApplicationDbContext>(
            options =>
            {
                options.UseSqlServer(connectionString);
            });

        services.AddScoped<IOrganizationRepository, OrganizationRepository>();
        services.AddScoped<ITokenService, JwtTokenService>();
        services.AddScoped<IIdentityService, IdentityService>();
        services.AddScoped<IUserIdentityService, UserIdentityService>();
        services.AddScoped<IRoleIdentityService, RoleIdentityService>();
        services.AddScoped<IFileStorageService, AzureBlobStorageService>();
        services.AddScoped<IPermissionRepository, PermissionRepository>();

        services.AddIdentityCore<ApplicationUser>(options =>
        {
            // We'll configure password rules later
        })
        .AddRoles<ApplicationRole>()
        .AddEntityFrameworkStores<ApplicationDbContext>()
        .AddDefaultTokenProviders();

        services.AddHttpContextAccessor();

        services.AddScoped<ICurrentUserService, CurrentUserService>();

        return services;
    }
}
