using WebForum.Domain.Models.UserModels;

namespace WebForum.Domain.Models;

public record Message
{
    public Guid SpaceId { get; init; }
    public Guid Id { get; init; }
    public User Author { get; init; }
    public string Content { get; init; }
    public DateTime CreationDate { get; init; }
}