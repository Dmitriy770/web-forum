using System.Net.Http.Json;
using WebForum.Frontend.HttpClients.Requests;

namespace WebForum.Frontend.HttpClients;

public class ProfileHttpClient(
    HttpClient httpClient
)
{
    public async Task Create(Guid id, string displayName, Uri? avatarUri, CancellationToken cancellationToken)
    {
        await httpClient.PostAsJsonAsync(
            $"api/profile/{id}",
            new CreateProfileRequest(displayName, avatarUri),
            cancellationToken
            );
    }
}