using WebForum.Frontend.Services.Models;

namespace WebForum.Frontend.States.AuthState.Actions;

public record AuthLoginResultAction(
    AuthInfo Info
);