using Microsoft.AspNetCore.Authorization;
using WebForum.Auth.Domain.Models;

namespace WebForum.Auth.Api.Authorization;

public sealed class HasPermissionAttribute(UserPermissions permissions)
    : AuthorizeAttribute(policy: permissions.ToString());