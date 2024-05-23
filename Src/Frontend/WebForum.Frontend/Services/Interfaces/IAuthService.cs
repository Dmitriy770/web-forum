using WebForum.Frontend.HttpClients.Responses;
using WebForum.Frontend.Models;

namespace WebForum.Frontend.Services.Interfaces;

public interface IAuthService
{
    public Task<Error?> Login(string login, string password, CancellationToken cancellationToken);
    
    public Task<Error?> Logout(CancellationToken cancellationToken);

    public Task<AuthInfo?> GetInfo(CancellationToken cancellationToken);
    
    public Task<bool> IsLogin(CancellationToken cancellationToken);
}