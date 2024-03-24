using WebForum.Frontend.Services.Models;

namespace WebForum.Frontend.Repositories.Interfaces;

public interface IAuthRepository
{
    public Task SaveAccessToken(AccessToken token, CancellationToken cancellationToken);

    public Task<AccessToken?> GetAccessToken(CancellationToken cancellationToken);

    public Task DeleteAccessToken(CancellationToken cancellationToken);
}