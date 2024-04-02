namespace WebForum.Frontend.States.AuthState.Actions;

public record AuthLogInStartAction(
    string Login,
    string Password
);