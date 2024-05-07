using WebForum.Core.Application.Interfaces;
using WebForum.Core.Domain.Models;

namespace WebForum.Core.Infrastructure.Repositories;

public class FakeProfileRepository : IProfileRepository
{
    private static readonly List<Profile> Profiles = [];
    
    public Task Save(Profile profile, CancellationToken cancellationToken)
    {
        Profiles.Add(profile);
        return Task.CompletedTask;
    }

    public Task Delete(Guid userId, CancellationToken cancellationToken)
    {
        if (Profiles.FirstOrDefault(p => p.UserId == userId) is { } profile)
        {
            Profiles.Remove(profile);
        }
        return Task.CompletedTask;
    }

    public Task<Profile?> FindByUserId(Guid userId, CancellationToken cancellationToken)
    {
        return Task.FromResult(Profiles.FirstOrDefault(p => p.UserId == userId));
    }
}