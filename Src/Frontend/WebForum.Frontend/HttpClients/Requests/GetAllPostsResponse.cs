using WebForum.Frontend.Models;

namespace WebForum.Frontend.HttpClients.Requests;

public record GetAllPostsResponse(
    IList<Post> Posts
);