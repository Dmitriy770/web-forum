namespace WebForum.Frontend.HttpClients.Requests;

public record CreatePostRequest(
    string Content,
    Guid? ParentId
);