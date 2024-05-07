using WebForum.Core.Domain.Models;

namespace WebForum.Core.Api.Responses;

public record GetAllPostsResponse(
    IList<Post> Posts
);