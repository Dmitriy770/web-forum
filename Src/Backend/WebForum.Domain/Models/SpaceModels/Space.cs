using WebForum.Domain.Models.UserModels;

namespace WebForum.Domain.Models.SpaceModels;

public record Space
{
    public Guid Id { get; init; }
    public string Name { get; init; }
    public string Description { get; init; }
    public DateTime CreationDate { get; init; }
    public SpaceType Type { get; init; }
    public User Author { get; init; }
    public ICollection<Tag> Tags { get; init; }
};