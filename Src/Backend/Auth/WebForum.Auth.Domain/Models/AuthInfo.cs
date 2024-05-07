namespace WebForum.Auth.Domain.Models;

public record AuthInfo
{
    public Guid Id { get; init; }
    public string Login { get; init; }
    public Permissions Permissions { get; init; }
    public DateTime ExpiresIn { get; init; }

    private AuthInfo(Guid id, string login, Permissions permissions, DateTime expiresIn)
    {
        Id = id;
        Login = login;
        Permissions = permissions;
        ExpiresIn = expiresIn;
    }

    public static AuthInfo Create(Guid id, string login, Permissions permissions, DateTime expiresIn)
    {
        return new AuthInfo(id, login, permissions, expiresIn);
    }
};