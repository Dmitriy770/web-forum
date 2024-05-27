namespace WebForum.Domain.Models;

public record User
{
    public Guid Id { get; init; }
    public string Login { get; init; }
    public string Name { get; init; }
    public Uri AvatarUri { get; init; }
}