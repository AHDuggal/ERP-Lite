using ERPLite.Application.Common.Settings;
using ERPLite.Application.Features.Authentication.Interfaces;
using ERPLite.Application.Features.Authentication.Services;
using ERPLite.Application.Features.Organizations.Interfaces;
using ERPLite.Application.Features.Organizations.Services;
using ERPLite.Application.Features.Roles.Interfaces;
using ERPLite.Application.Features.Roles.Services;
using ERPLite.Application.Features.Users.Interfaces;
using ERPLite.Application.Features.Users.Services;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ERPLite.Application.DependencyInjection;

public static class ApplicationExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IOrganizationService, OrganizationService>();
       
        services.AddScoped<IAuthenticationService,  AuthenticationService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IRoleService, RoleService>();
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        return services;
    }
}
