using System.Net;
using Blazored.LocalStorage;
using WebForum.Frontend.HttpClients;
using WebForum.Frontend.HttpClients.Responses;
using WebForum.Frontend.Models;
using WebForum.Frontend.Services.Interfaces;

namespace WebForum.Frontend.Services;

public class AuthService(
    AuthHttpClient httpClient,
    ILocalStorageService localStorageService
) : IAuthService
{
    private const string AuthInfoKey = "auth-info";
    
    public async Task<Error?> Login(string login, string password, CancellationToken cancellationToken)
    {
        var (authInfo, error) = await httpClient.Login(login, password, cancellationToken);
        if (authInfo is null)
        {
            return error;
        }

        await localStorageService.SetItemAsync(AuthInfoKey, authInfo, cancellationToken);
        
        return null;
    }

    public async Task<Error?> Logout(CancellationToken cancellationToken)
    {
        var error = await httpClient.Logout(cancellationToken);
        if (error is not null && error.Status != (int)HttpStatusCode.Unauthorized)
        {
            return error;
        }

        await localStorageService.RemoveItemAsync(AuthInfoKey, cancellationToken);

        return null;
    }

    public async Task<AuthInfo?> GetInfo(CancellationToken cancellationToken)
    {
        return await localStorageService.GetItemAsync<AuthInfo>(AuthInfoKey, cancellationToken);
    }
    
    public async Task<bool> IsLogin(CancellationToken cancellationToken)
    {
        return await GetInfo(cancellationToken) is not null;
    }
}