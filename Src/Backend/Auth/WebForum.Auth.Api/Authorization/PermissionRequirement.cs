using Microsoft.AspNetCore.Authorization;

namespace WebForum.Auth.Api.Authorization;

public sealed class PermissionRequirement(string permissions) : IAuthorizationRequirement
{
    public readonly string Permissions = permissions;
}