namespace WebForum.Frontend.Services.Interfaces;

public interface IAuthService
{
    public Task Login(string login, string password);
    
    public Task<bool> IsLogin();
}