using Fluxor;
using WebForum.Frontend.States.AuthState.Actions;

namespace WebForum.Frontend.States.AuthState;

public static class AuthReducers
{
    [ReducerMethod]
    public static AuthState ReduceLoginStartAction(AuthState state, AuthLoginStartAction action) =>
        new AuthState
        {
            Status = AuthStatus.Loading
        };

    [ReducerMethod]
    public static AuthState ReduceLoginResultAction(AuthState state, AuthLoginResultAction action) =>
        new AuthState
        {
            Status = AuthStatus.LogIn,
            Info = action.Info
        };
}
