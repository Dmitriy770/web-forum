using WebForum.Frontend.Services.Models;

namespace WebForum.Frontend.Repositories.Interfaces;

public interface IAuthRepository
{
    public Task SaveAccessToken(AuthInfo token, CancellationToken cancellationToken);

    public Task<AuthInfo?> GetAccessToken(CancellationToken cancellationToken);

    public Task DeleteAccessToken(CancellationToken cancellationToken);
}