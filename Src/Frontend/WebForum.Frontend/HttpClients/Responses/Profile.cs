namespace WebForum.Frontend.HttpClients.Responses;

public record Profile
{
    public Guid UserId { get; init; }
    public string DisplayName { get; init; }
    public Uri? AvatarUri { get; init; }
}