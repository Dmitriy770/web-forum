namespace WebForum.Auth.Domain.Models;

public sealed class User
{
    public Guid Id { get; }
    public string Login { get; }
    public string HashedPassword { get; }
    
    public UserPermissions Permissions { get; }

    private User(Guid id, string login, string hashedPassword, UserPermissions permissions)
    {
        Id = id;
        Login = login;
        HashedPassword = hashedPassword;
        Permissions = permissions;
    }

    public static User Create(Guid id, string login, string hashPassword, UserPermissions permissions)
    {
        return new User(id, login, hashPassword, permissions);
    }
}