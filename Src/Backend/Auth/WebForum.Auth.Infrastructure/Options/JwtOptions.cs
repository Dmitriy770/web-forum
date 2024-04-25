namespace WebForum.Auth.Infrastructure.Options;

public record JwtOptions
{
    public string SecretKey { get; init; }
    public int ExpiresHours { get; init; }
}