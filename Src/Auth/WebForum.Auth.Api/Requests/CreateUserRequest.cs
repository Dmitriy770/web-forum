namespace WebForum.Auth.Api.Requests;

public record CreateUserRequest(
    string Login,
    string Password
);