using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ERPLite.Application.Features.Organizations.Interfaces;
using ERPLite.Application.Features.Organizations.Services;
using Microsoft.Extensions.DependencyInjection;

using FluentValidation;
using System.Reflection;

namespace ERPLite.Application.DependencyInjection;

public static class ApplicationExtensions
{
    public static IServiceCollection AddApplication(
     this IServiceCollection services)
    {
        services.AddScoped<IOrganizationService, OrganizationService>();
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());


        return services;
    }
}
