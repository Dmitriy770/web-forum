namespace WebForum.Core.Domain.Models;

public record Profile
{
    public Guid UserId { get; init; }
    public string DisplayName { get; init; }
    public Uri? AvatarUri { get; init; }
}