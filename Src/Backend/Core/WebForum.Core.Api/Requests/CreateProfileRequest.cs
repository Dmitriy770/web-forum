namespace WebForum.Core.Api.Requests;

public record CreateProfileRequest(
    string DisplayName,
    Uri? AvatarUri
);