using WebForum.Auth.Domain.Models;

namespace WebForum.Auth.Application.Interfaces;

public interface IJwtProvider
{
    public (string token, DateTime expiresIn) GenerateToken(User user);
}