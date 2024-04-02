using System.Net;
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

        var http = scope.ServiceProvider.GetService<AuthHttpClient>()!;

        try
        {
            var authInfo = await http.SignIn(action.Login, action.Password, CancellationToken.None);
            dispatcher.Dispatch(new AuthLogInSuccessAction(authInfo));
            navigation.NavigateTo("");
        }
        catch (HttpRequestException e) when (e.StatusCode == HttpStatusCode.BadRequest)
        {
            dispatcher.Dispatch(new AuthErrorAction("Wrong password or login"));
        }
        catch (HttpRequestException e)
        {
            dispatcher.Dispatch(new AuthErrorAction(e.Message));
        }
    }

    [EffectMethod]
    public async Task HandleAuthLogOutStartAction(AuthLogOutStartAction action, IDispatcher dispatcher)
    {
        using var scope = serviceScopeFactory.CreateScope();

        var http = scope.ServiceProvider.GetService<AuthHttpClient>();

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