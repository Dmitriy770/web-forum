using WebForum.Frontend.HttpClients.Responses;

namespace WebForum.Frontend.Models;

public record Post
{
    public Guid Id { get; init; }
    public string Content { get; init; }
    public Guid? ParentId { get; init; }
    public DateTime CreationDate { get; init; }
    public Profile Profile { get; init; }
    public bool IsVisible { get; init; }
}