using WebForum.Frontend.Services.Models;

namespace WebForum.Frontend.Repositories.Interfaces;

public interface IAuthRepository
{
    public Task SaveAccessToken(AccessToken token);

    public Task<AccessToken?> GetAccessToken();
}