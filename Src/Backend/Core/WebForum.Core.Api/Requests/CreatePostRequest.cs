namespace WebForum.Core.Api.Requests;

public record CreatePostRequest(
    string Content,
    Guid? ParentId
);