using WebForum.Auth.Domain.Models;

namespace WebForum.Auth.Application.Interfaces;

public interface IJwtProvider
{
    public string GenerateToken(User user);
}