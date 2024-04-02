namespace WebForum.Frontend.Models;

public record AuthInfo(
    Guid Id,
    string Login,
    UserPermissions Permissions,
    DateTime ExpiresIn
);