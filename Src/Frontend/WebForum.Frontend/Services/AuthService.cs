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
    public async Task LogIn(string login, string password, CancellationToken cancellationToken)
    {
        var response = await httpClient.PostAsJsonAsync(
            "/api/auth/access-token",
            new { login, password },
            cancellationToken);

        var accessToken = await response.Content.ReadFromJsonAsync<AccessToken>(cancellationToken);
        if (accessToken is null)
        {
            throw new Exception();
        }

        await authRepository.SaveAccessToken(accessToken, cancellationToken);
    }

    public async Task LogOut(CancellationToken cancellationToken)
    {
        await httpClient.DeleteAsync(
            "api/auth/access-token",
            cancellationToken);
        await authRepository.DeleteAccessToken(cancellationToken);
    }

    public async Task<bool> IsLogin(CancellationToken cancellationToken)
    {
        var token = await authRepository.GetAccessToken(cancellationToken);
        if (token is null)
        {
            return false;
        }

        return token.ExpiresIn > DateTime.Now;
    }
}