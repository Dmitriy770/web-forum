namespace WebForum.Frontend.Services.Interfaces;

public interface IAuthService
{
    public Task LogIn(string login, string password, CancellationToken cancellationToken);

    public Task LogOut(CancellationToken cancellationToken);
    
    public Task<bool> IsLogin(CancellationToken cancellationToken);
}