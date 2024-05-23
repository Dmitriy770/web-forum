using System.Net;
using System.Net.Http.Json;
using WebForum.Core.Domain.Models;
using WebForum.Frontend.HttpClients.Requests;
using WebForum.Frontend.HttpClients.Responses;

namespace WebForum.Frontend.HttpClients;

public class ProfileHttpClient(
    HttpClient httpClient
)
{
    private readonly Uri _defaultAvatar = new("/avatar.jpg", UriKind.RelativeOrAbsolute);
    
    public async Task Create(Guid id, string displayName, Uri? avatarUri, CancellationToken cancellationToken)
    {
        await httpClient.PostAsJsonAsync(
            $"api/core/profile/{id}",
            new CreateProfileRequest(displayName, avatarUri),
            cancellationToken
            );
    }
    
    public async Task<(Profile?, Error?)> Get(Guid userId, CancellationToken cancellationToken)
    {
        var response = await httpClient.GetAsync(
            $"api/core/profile/{userId}",
            cancellationToken);
        
        Console.WriteLine(response.StatusCode);
        Console.WriteLine(await response.Content.ReadAsStringAsync(cancellationToken));
        
        switch (response.StatusCode)
        {
            case HttpStatusCode.OK:
                var profile = (await response.Content.ReadFromJsonAsync<Profile>(cancellationToken))!;
                if (profile.AvatarUri is null)
                {
                    return (profile with { AvatarUri = _defaultAvatar }, null);
                }
                return (profile, null);
            
            default:
                var error = await response.Content.ReadFromJsonAsync<Error>(cancellationToken);
                return (null, error);
        }
    }
}