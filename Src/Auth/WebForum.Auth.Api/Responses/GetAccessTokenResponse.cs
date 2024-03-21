namespace WebForum.Auth.Api.Responses;

public record GetAccessTokenResponse(
    DateTime ExpiresIn
);