using System.Text.Json.Serialization;

namespace WebForum.Frontend.Services.Models;

public record AuthInfo(
    Guid Id,
    string Login,
    UserPermissions Permissions,
    DateTime ExpiresIn
);