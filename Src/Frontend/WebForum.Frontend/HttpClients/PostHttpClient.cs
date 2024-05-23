using System.Collections;
using System.Net;
using System.Net.Http.Json;
using WebForum.Frontend.HttpClients.Requests;
using WebForum.Frontend.HttpClients.Responses;
using WebForum.Frontend.Models;

namespace WebForum.Frontend.HttpClients;

public class PostHttpClient(
    HttpClient httpClient
)
{
    private readonly Uri _defaultAvatar = new("/avatar.jpg", UriKind.RelativeOrAbsolute);
    
    public async Task<Error?> Create(string content, Guid? parentId, CancellationToken cancellationToken)
    {
        var response = await httpClient.PostAsJsonAsync(
            "api/post",
            new CreatePostRequest(content, parentId),
            cancellationToken);

        return response.StatusCode switch
        {
            HttpStatusCode.OK => null,
            _ => await response.Content.ReadFromJsonAsync<Error>(cancellationToken)
        };
    }

    public async Task<(IEnumerable<Post>, Error?)> GetAll(int skip, int take, CancellationToken cancellationToken)
    {
        var response = await httpClient.GetAsync($"api/post?take={take}&skip={skip}", cancellationToken);

        return response.StatusCode switch {
            HttpStatusCode.OK => (PreparePosts(await response.Content.ReadFromJsonAsync<GetAllPostsResponse>(cancellationToken)), null),
            _ => (new List<Post>(), await response.Content.ReadFromJsonAsync<Error>(cancellationToken))
        };
    }

    public async Task<(IEnumerable<Post>, Error?)> GetByParentId(Guid parentId, int skip, int take, CancellationToken cancellationToken)
    {
        var response = await httpClient.GetAsync($"api/post?take={take}&skip={skip}$parentId={parentId}", cancellationToken);
        
        return response.StatusCode switch {
            HttpStatusCode.OK => (PreparePosts(await response.Content.ReadFromJsonAsync<GetAllPostsResponse>(cancellationToken)), null),
            _ => (new List<Post>(), await response.Content.ReadFromJsonAsync<Error>(cancellationToken))
        };
    }

    private IEnumerable<Post> PreparePosts(GetAllPostsResponse response)
    {
        return response.Posts.Select(post => post with
        {
            Profile = post.Profile with { AvatarUri =  post.Profile.AvatarUri ?? _defaultAvatar}
        });
    }
}