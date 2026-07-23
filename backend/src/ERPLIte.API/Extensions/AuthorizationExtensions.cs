using ERPLite.API.Authorization;
using Microsoft.AspNetCore.Authorization;

namespace ERPLite.API.Extensions;

public static class AuthorizationExtensions
{
    public static IServiceCollection AddAuthorizationConfiguration(
        this IServiceCollection services)
    {
        services.AddAuthorization();

        services.AddScoped<
            IAuthorizationHandler,
            PermissionAuthorizationHandler>();

        services.AddSingleton<
            IAuthorizationPolicyProvider,
            PermissionPolicyProvider>();

        return services;
    }
}