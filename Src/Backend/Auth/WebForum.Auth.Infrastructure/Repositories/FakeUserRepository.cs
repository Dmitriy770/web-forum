using WebForum.Auth.Application.Interfaces;
using WebForum.Auth.Domain.Models;

namespace WebForum.Auth.Infrastructure.Repositories;

public class FakeUserRepository : IUserRepository
{
    private static List<User> _users = []; 
    public Task Save(User user, CancellationToken cancellationToken)
    {
        _users.Add(user);
        return Task.CompletedTask;
    }

    public Task Delete(Guid id, CancellationToken cancellationToken)
    {
        _users = _users.Where(u => u.Id != id).ToList();
        return Task.CompletedTask;
    }

    public Task<User?> FindByUserId(Guid id, CancellationToken cancellationToken)
    {
        return Task.FromResult(_users.FirstOrDefault(u => u.Id == id));
    }

    public Task<User?> FindByLogin(string login, CancellationToken cancellationToken)
    {
        return Task.FromResult(_users.FirstOrDefault(u => string.Compare(u.Login, login, StringComparison.Ordinal) == 0));
    }
}