using Fluxor;
using WebForum.Frontend.Services.Models;
using WebForum.Frontend.States.AuthState.Actions;

namespace WebForum.Frontend.States.AuthState;

[FeatureState]
public record AuthState
{
    public AuthStatus Status { get; init; } = AuthStatus.LogOut;
    public AuthInfo? Info { get; init; }

    public AuthState()
    {
    }
}