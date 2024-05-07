using WebForum.Auth.Application.Interfaces;
using WebForum.Auth.Domain.Models;

namespace WebForum.Auth.Infrastructure.Repositories;

public sealed class FakeUserRepository : IUserRepository
{
    private static readonly List<User> Users = []; 
    public Task Save(User user, CancellationToken cancellationToken)
    {
        Users.Add(user);
        return Task.CompletedTask;
    }

    public Task Delete(Guid id, CancellationToken cancellationToken)
    {
        if (Users.FirstOrDefault(u => u.Id == id) is { } user)
        {
            Users.Remove(user);
        }
        return Task.CompletedTask;
    }

    public Task<User?> FindByUserId(Guid id, CancellationToken cancellationToken)
    {
        return Task.FromResult(Users.FirstOrDefault(u => u.Id == id, null));
    }

    public Task<User?> FindByLogin(string login, CancellationToken cancellationToken)
    {
        return Task.FromResult(Users.FirstOrDefault(u => string.Compare(u.Login, login, StringComparison.Ordinal) == 0, null));
    }
}