using System.Net;
using System.Net.Http.Json;
using WebForum.Frontend.HttpClients.Requests;
using WebForum.Frontend.HttpClients.Responses;

namespace WebForum.Frontend.HttpClients;

public class PostHttpClient(
    HttpClient httpClient
)
{
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
}