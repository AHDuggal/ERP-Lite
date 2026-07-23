using ERPLite.Application.Features.Authorization.Interfaces;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace ERPLite.API.Authorization;

public sealed class PermissionAuthorizationHandler
    : AuthorizationHandler<PermissionRequirement>
{
    private readonly IPermissionService _permissionService;

    public PermissionAuthorizationHandler(
        IPermissionService permissionService)
    {
        _permissionService = permissionService;
    }

    protected override async Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        PermissionRequirement requirement)
    {
        var userIdValue =
            context.User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (string.IsNullOrWhiteSpace(userIdValue))
        {
            return;
        }

        if (!Guid.TryParse(userIdValue, out var userId))
        {
            return;
        }

        var permissions =
            await _permissionService.GetPermissionsAsync(
                userId,
                CancellationToken.None);

        if (permissions.Contains(requirement.Permission))
        {
            context.Succeed(requirement);
        }
    }
}