using System.Net;
using System.Net.Http.Json;
using Common.Nullable;
using FluentResults;
using WebForum.Frontend.HttpClients.Responses;
using WebForum.Frontend.Models;

namespace WebForum.Frontend.HttpClients;

public class AuthHttpClient(
    HttpClient http
)
{
    public async Task<Result<AuthInfo>> SignIn(string login, string password, CancellationToken cancellationToken)
    {
        var response = await http.PostAsJsonAsync(
            "/api/auth/access-token",
            new { login, password },
            cancellationToken);

        switch (response.StatusCode)
        {
            case HttpStatusCode.OK:
                var authInfo = await response.Content.ReadFromJsonAsync<AuthInfo>(cancellationToken);
                return authInfo.Match(
                    Result.Ok,
                    () => Result.Fail("unexpected error")
                );
            case HttpStatusCode.BadRequest:
                return Result.Fail("invalid password or login");
            default:
                return Result.Fail("unexpected error");
        }
    }

    public async Task SignOut(CancellationToken cancellationToken)
    {
        await http.DeleteAsync(
            "/api/auth/access-token",
            cancellationToken);
    }

    public async Task<Guid> SignUp(string login, string password, CancellationToken cancellationToken)
    {
        var response = await http.PostAsJsonAsync(
            "/api/auth/user",
            new { login, password },
            cancellationToken);

        var content = (await response.Content.ReadFromJsonAsync<CreateUserResponse>(cancellationToken))!;
        return content.UserId;
    }
}