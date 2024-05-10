using System.Net.Http.Json;
using WebForum.Core.Domain.Models;
using WebForum.Frontend.HttpClients.Requests;

namespace WebForum.Frontend.HttpClients;

public class ProfileHttpClient(
    HttpClient httpClient
)
{
    public async Task Create(Guid id, string displayName, Uri? avatarUri, CancellationToken cancellationToken)
    {
        await httpClient.PostAsJsonAsync(
            $"api/core/profile/{id}",
            new CreateProfileRequest(displayName, avatarUri),
            cancellationToken
            );
    }
    
    public async Task<Profile> Get(Guid userId, CancellationToken cancellationToken)
    {
        var profile = await httpClient.GetFromJsonAsync<Profile>(
            $"api/core/profile/{userId}",
            cancellationToken);

        return profile!;
    }
}