using System.Net.Http.Json;
using WebForum.Frontend.Models;

namespace WebForum.Frontend.HttpClients;

public class AuthHttpClient(
    HttpClient http
)
{
    public async Task<AuthInfo> SignIn(string login, string password, CancellationToken cancellationToken)
    {
        var response = await http.PostAsJsonAsync(
            "/api/auth/access-token",
            new { login, password },
            cancellationToken);

        return await response.Content.ReadFromJsonAsync<AuthInfo>();
    }

    public async Task SignOut(CancellationToken cancellationToken)
    {
        await http.DeleteAsync(
            "api/auth/access-token",
            cancellationToken);
    }

    public async Task SignUp(string login, string password, CancellationToken cancellationToken)
    {
        await http.PostAsJsonAsync(
            "/user",
            new { login, password },
            cancellationToken);
    }
}