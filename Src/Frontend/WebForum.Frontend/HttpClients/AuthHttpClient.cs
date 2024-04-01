using System.Net.Http.Json;
using WebForum.Frontend.Services.Models;

namespace WebForum.Frontend.HttpClients;

public class AuthHttpClient(
    HttpClient http
)
{
    public async Task<AuthInfo> LogIn(string login, string password, CancellationToken cancellationToken)
    {
        var response = await http.PostAsJsonAsync(
            "/api/auth/access-token",
            new { login, password },
            cancellationToken);

        return await response.Content.ReadFromJsonAsync<AuthInfo>();
    }

    public async Task LogOut(CancellationToken cancellationToken)
    {
        await http.DeleteAsync(
            "api/auth/access-token",
            cancellationToken);
    }
}