namespace WebForum.Frontend.States.AuthState.Actions;

public record AuthLoginStartAction(
    string Login,
    string Password
);