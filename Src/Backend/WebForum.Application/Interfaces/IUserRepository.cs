using WebForum.Domain.Models;

namespace WebForum.Application.Interfaces;

public interface IUserRepository
{
    public Task<User> Get
    
    public Task<User> GetById(Guid id, CancellationToken cancellationToken);
}