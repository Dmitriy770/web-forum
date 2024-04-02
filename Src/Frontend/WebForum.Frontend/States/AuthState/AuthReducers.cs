using Fluxor;
using WebForum.Frontend.States.AuthState.Actions;

namespace WebForum.Frontend.States.AuthState;

public static class AuthReducers
{
    [ReducerMethod]
    public static AuthState ReduceLogInStartAction(AuthState state, AuthLogInStartAction action) =>
        new AuthState
        {
            IsLoading = true,
        };

    [ReducerMethod]
    public static AuthState ReduceLogInSuccessAction(AuthState state, AuthLogInSuccessAction action) =>
        new AuthState
        {
            IsLoading = false,
            Info = action.Info
        };
    
    [ReducerMethod]
    public static AuthState ReduceAuthLogOutStartAction(AuthState state, AuthLogOutStartAction action) =>
        state with
        {
            IsLoading = true
        };

    [ReducerMethod]
    public static AuthState ReduceAuthLogOutSuccess(AuthState state, AuthLogOutSuccessAction action) =>
        new AuthState
        {
            IsLoading = false
        };

    [ReducerMethod]
    public static AuthState ReduceAuthErrorAction(AuthState state, AuthErrorAction action) =>
        state with
        {
            IsLoading = false,
            Error = action.Error
        };
}