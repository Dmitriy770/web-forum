namespace WebForum.Domain.Models.UserModels;

public class Credential
{
    public Guid Id { get; init; }
    public string Login { get; init; }
    public string PasswordHash { get; init; }
}