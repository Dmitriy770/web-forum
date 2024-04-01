namespace WebForum.Auth.Api.Responses;

public record GetAccessTokenResponse(
    Guid Id,
    string Login,
    string Permissions,
    DateTime ExpiresIn
);