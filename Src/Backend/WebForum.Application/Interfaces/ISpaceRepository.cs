using WebForum.Application.Entities;
using WebForum.Domain.Models;

namespace WebForum.Application.Interfaces;

public interface ISpaceRepository
{
    public Task Add(SpaceEntity space, CancellationToken cancellationToken);
    
    public Task Update(SpaceEntity space, CancellationToken cancellationToken);

    public Task<SpaceEntity> FindById(Guid id, CancellationToken cancellationToken);
    
    public IAsyncEnumerable<SpaceEntity> GetAll(CancellationToken cancellationToken);
}