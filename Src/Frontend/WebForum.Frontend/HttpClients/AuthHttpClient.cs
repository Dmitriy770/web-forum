using System.Net;
using System.Net.Http.Json;
using WebForum.Frontend.HttpClients.Responses;
using WebForum.Frontend.Models;

namespace WebForum.Frontend.HttpClients;

public class AuthHttpClient(
    HttpClient http
)
{
    public async Task<(AuthInfo?, Error?)> Login(string login, string password, CancellationToken cancellationToken)
    {
        var response = await http.PostAsJsonAsync(
            "/api/auth/access-token",
            new { login, password },
            cancellationToken);

        return response.StatusCode switch {
            HttpStatusCode.OK => (await response.Content.ReadFromJsonAsync<AuthInfo>(cancellationToken), null),
            _ => (null, await response.Content.ReadFromJsonAsync<Error>(cancellationToken))
        };
    }

    public async Task<Error?> Logout(CancellationToken cancellationToken)
    {
        var response = await http.DeleteAsync(
            "/api/auth/access-token",
            cancellationToken);

        return response.StatusCode switch
        {
            HttpStatusCode.OK => null,
            _ => await response.Content.ReadFromJsonAsync<Error>(cancellationToken)
        };
    }

    public async Task<Guid> Registration(string login, string password, CancellationToken cancellationToken)
    {
        var response = await http.PostAsJsonAsync(
            "/api/auth/user",
            new { login, password },
            cancellationToken);

        var content = (await response.Content.ReadFromJsonAsync<CreateUserResponse>(cancellationToken))!;
        return content.UserId;
    }
}