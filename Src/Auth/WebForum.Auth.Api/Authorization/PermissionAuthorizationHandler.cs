using Microsoft.AspNetCore.Authorization;
using WebForum.Auth.Domain.Models;

namespace WebForum.Auth.Api.Authorization;

public sealed class PermissionAuthorizationHandler : AuthorizationHandler<PermissionRequirement>
{
    protected override Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        PermissionRequirement requirement)
    {
        var permissions = context.User.Claims
            .FirstOrDefault(c => string.Compare(c.Type, nameof(User.Permissions), StringComparison.Ordinal) == 0);
        if (permissions is not null && HasPermissions(permissions.Value, requirement.Permissions))
        {
            context.Succeed(requirement);
        }
        
        return Task.CompletedTask;
    }

    private static bool HasPermissions(string current, string target)
    {
        var currentPermissions = current.Split(", ");
        var targetPermissions = target.Split(", ");
        
        foreach (var targetPermission in targetPermissions)
        {
            if (!currentPermissions.Contains(targetPermission))
            {
                return false;
            }
        }
        
        return true;
    }
}