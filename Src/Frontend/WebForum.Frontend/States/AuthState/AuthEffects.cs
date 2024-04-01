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
    public async Task HandleAuthLoginStartAction(AuthLoginStartAction action, IDispatcher dispatcher)
    {
        using var scope = serviceScopeFactory.CreateScope();

        var http = scope.ServiceProvider.GetService<AuthHttpClient>()!;
        var authInfo = await http.LogIn(action.Login, action.Password, CancellationToken.None);
        
        dispatcher.Dispatch(new AuthLoginResultAction(authInfo));
        navigation.NavigateTo("");
    }
}