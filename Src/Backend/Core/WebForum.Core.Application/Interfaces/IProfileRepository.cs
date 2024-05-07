using WebForum.Core.Domain.Models;

namespace WebForum.Core.Application.Interfaces;

public interface IProfileRepository
{
    public Task Save(Profile profile, CancellationToken cancellationToken);

    public Task Delete(Guid userId, CancellationToken cancellationToken);

    public Task<Profile?> FindByUserId(Guid userId, CancellationToken cancellationToken);
}