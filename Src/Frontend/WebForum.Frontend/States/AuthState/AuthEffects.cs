using Common.FluentResult;
using Fluxor;
using Microsoft.AspNetCore.Components;
using WebForum.Frontend.HttpClients;
using WebForum.Frontend.States.AuthState.Actions;

namespace WebForum.Frontend.States.AuthState;

public class AuthEffects(
    IServiceScopeFactory serviceScopeFactory,
    NavigationManager navigation
)
{
    [EffectMethod]
    public async Task HandleAuthLogInStartAction(AuthLogInStartAction action, IDispatcher dispatcher)
    {
        using var scope = serviceScopeFactory.CreateScope();

        var http = scope.ServiceProvider.GetRequiredService<AuthHttpClient>();

        var authResult = await http.SignIn(action.Login, action.Password, CancellationToken.None);

        authResult.Match(authInfo =>
        {
            dispatcher.Dispatch(new AuthLogInSuccessAction(authInfo));
            navigation.NavigateTo("");
        },
        errors =>
        {
            dispatcher.Dispatch(new AuthErrorAction(string.Join(", ", errors)));
        });
    }

    [EffectMethod]
    public async Task HandleAuthLogOutStartAction(AuthLogOutStartAction action, IDispatcher dispatcher)
    {
        using var scope = serviceScopeFactory.CreateScope();

        var http = scope.ServiceProvider.GetRequiredService<AuthHttpClient>();

        try
        {
            await http.SignOut(CancellationToken.None);
            dispatcher.Dispatch(new AuthLogOutSuccessAction());
            navigation.NavigateTo("/login");
        }
        catch (HttpRequestException e)
        {
            dispatcher.Dispatch(new AuthErrorAction(e.Message));
        }
    }
}