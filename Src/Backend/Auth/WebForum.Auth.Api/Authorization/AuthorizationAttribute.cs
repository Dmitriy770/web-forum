using WebForum.Auth.Domain.Models;

namespace WebForum.Auth.Api.Authorization;

[AttributeUsage(AttributeTargets.Method)]
public class AuthorizationAttribute(
    Permissions permissions
) : Attribute
{
    public Permissions Permissions { get; } = permissions;
}