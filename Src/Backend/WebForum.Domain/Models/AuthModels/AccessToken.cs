namespace WebForum.Domain.Models.AuthModels;

public record AccessToken(
    DateTime ExpiresIn
);