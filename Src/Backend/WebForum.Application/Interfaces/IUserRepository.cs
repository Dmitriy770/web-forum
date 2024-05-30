using WebForum.Domain.Models;
using WebForum.Domain.Models.UserModels;

namespace WebForum.Application.Interfaces;

public interface IUserRepository
{
    public Task Add(User user, CancellationToken cancellationToken);

    public Task Update(User user, CancellationToken cancellationToken);
    
    public Task<User> GetById(Guid id, CancellationToken cancellationToken);
    
    public Task<User> GetByLogin(string login, CancellationToken cancellationToken);
    
}