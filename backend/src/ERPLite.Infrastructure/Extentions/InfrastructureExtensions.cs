using ERPLite.Application.Features.Authentication.Interfaces;
using ERPLite.Application.Features.Organizations.Interfaces;
using ERPLite.Application.Features.Roles.Interfaces;
using ERPLite.Domain.Entities;
using ERPLite.Infrastructure.Identity;
using ERPLite.Infrastructure.Persistence;
using ERPLite.Infrastructure.Repositories;
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
        services.AddScoped<IRoleIdentityService, RoleIdentityService>();

        services.AddIdentityCore<ApplicationUser>(options =>
        {
            // We'll configure password rules later
        })
        .AddRoles<ApplicationRole>()
        .AddEntityFrameworkStores<ApplicationDbContext>()
        .AddDefaultTokenProviders();        

        return services;
    }
}
