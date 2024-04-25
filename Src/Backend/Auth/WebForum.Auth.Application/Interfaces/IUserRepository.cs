using WebForum.Auth.Domain.Models;

namespace WebForum.Auth.Application.Interfaces;

public interface IUserRepository
{
    public Task Save(User user, CancellationToken cancellationToken);

    public Task Delete(Guid id, CancellationToken cancellationToken);

    public Task<User?> FindByUserId(Guid id, CancellationToken cancellationToken);

    public Task<User?> FindByLogin(string login, CancellationToken cancellationToken);
}