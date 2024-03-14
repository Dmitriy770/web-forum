namespace WebForum.Auth.Infrastructure.Options;

public class JwtOptions
{
    public string SecretKey { get; init; }
    public int ExpiresHours { get; init; }
}