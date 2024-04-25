namespace WebForum.Auth.Api.Requests;

public record GetAccessTokenRequest(
    string Login,
    string Password
);