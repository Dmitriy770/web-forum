using WebForum.Domain.Models.AuthModels;
using WebForum.Domain.Models.UserModels;

namespace WebForum.Application.Interfaces;

public interface IJwtProvider
{
    public (string token, AccessToken accessToken) GenerateToken(Credential credential);

    public Task<Guid?> ValidateToken(string accessToken);
}