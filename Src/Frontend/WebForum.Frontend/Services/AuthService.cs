using System.Net.Http.Json;
using WebForum.Frontend.Repositories.Interfaces;
using WebForum.Frontend.Services.Interfaces;
using WebForum.Frontend.Services.Models;

namespace WebForum.Frontend.Services;

public class AuthService(
    HttpClient httpClient,
    IAuthRepository authRepository
) : IAuthService
{
    public async Task Login(string login, string password)
    {
        var response = await httpClient.PostAsJsonAsync(
            "/api/auth/access-token",
            new { login, password });

        var accessToken = await response.Content.ReadFromJsonAsync<AccessToken>();
        if (accessToken is null)
        {
            throw new Exception();
        }

        await authRepository.SaveAccessToken(accessToken);
    }

    public async Task<bool> IsLogin()
    {
        var token = await authRepository.GetAccessToken();
        if (token is null)
        {
            return false;
        }

        return token.ExpiresIn > DateTime.Now;
    }
}