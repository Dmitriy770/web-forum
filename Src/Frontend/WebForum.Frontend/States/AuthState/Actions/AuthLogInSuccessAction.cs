using WebForum.Frontend.Models;

namespace WebForum.Frontend.States.AuthState.Actions;

public record AuthLogInSuccessAction(
    AuthInfo Info
);