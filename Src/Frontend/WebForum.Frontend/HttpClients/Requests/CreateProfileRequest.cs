namespace WebForum.Frontend.HttpClients.Requests;

public record CreateProfileRequest(
    string DisplayName,
    Uri? AvatarUri
);