using Blazored.LocalStorage;
using WebForum.Frontend.Repositories.Interfaces;
using WebForum.Frontend.Services.Models;

namespace WebForum.Frontend.Repositories;

public class AuthRepository(
    ILocalStorageService localStorage
) : IAuthRepository
{
    private const string AccessTokenKey = "accessToken";
    
    public async Task SaveAccessToken(AuthInfo token, CancellationToken cancellationToken)
    {
        await localStorage.SetItemAsync(AccessTokenKey, token, cancellationToken);
    }

    public async Task<AuthInfo?> GetAccessToken(CancellationToken cancellationToken)
    {
        return await localStorage.GetItemAsync<AuthInfo>(AccessTokenKey, cancellationToken);
    }

    public async Task DeleteAccessToken(CancellationToken cancellationToken)
    {
        await localStorage.RemoveItemAsync(AccessTokenKey, cancellationToken);
    }
}