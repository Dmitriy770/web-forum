namespace WebForum.Auth.Domain.Models;

public sealed class User
{
    public Guid Id { get; }
    public string Login { get; }
    public string HashPassword { get; }
    
    public UserPermissions Permissions { get; }

    private User(Guid id, string login, string hashPassword, UserPermissions permissions)
    {
        Id = id;
        Login = login;
        HashPassword = hashPassword;
        Permissions = permissions;
    }

    public static User Create(Guid id, string login, string hashPassword, UserPermissions permissions)
    {
        return new User(id, login, hashPassword, permissions);
    }
}