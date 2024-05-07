using WebForum.Auth.Domain.Models;

namespace WebForum.Auth.Application.Interfaces;

public interface IJwtProvider
{
    public (string token, AuthInfo authInfo) GenerateToken(User user);

    public Task<(bool isValid, Guid userId)> ValidateToken(string accessToken);

}