using Blazored.LocalStorage;
using WebForum.Frontend.Repositories.Interfaces;
using WebForum.Frontend.Services.Models;

namespace WebForum.Frontend.Repositories;

public class AuthRepository(
    ILocalStorageService localStorage
) : IAuthRepository
{
    private const string AccessTokenKey = "accessToken";
    
    public async Task SaveAccessToken(AccessToken token)
    {
        await localStorage.SetItemAsync(AccessTokenKey, token);
    }

    public async Task<AccessToken?> GetAccessToken()
    {
        return await localStorage.GetItemAsync<AccessToken>(AccessTokenKey);
    }
}