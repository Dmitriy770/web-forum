namespace WebForum.Domain.Models.UserModels;

public record UserDraft
{
    public string Login { get; init; }
    public string? Name { get; init; }
    public Uri? AvatarUri { get; init; }
    public string Password { get; init; }
};