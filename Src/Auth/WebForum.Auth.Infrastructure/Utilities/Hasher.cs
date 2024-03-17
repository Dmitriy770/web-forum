using WebForum.Auth.Application.Interfaces;

namespace WebForum.Auth.Infrastructure.Utilities;

public sealed class Hasher : IHasher
{
    public string Hash(string password)
    {
        return BCrypt.Net.BCrypt.EnhancedHashPassword(password);
    }

    public bool Verify(string password, string hashedPassword)
    {
        return BCrypt.Net.BCrypt.EnhancedVerify(password, hashedPassword);
    }
}