using WebForum.Domain.Models;

namespace WebForum.Application.Services.Interfaces;

public interface ISpaceService
{
    public Task<Space> GetSpace(Guid id, CancellationToken cancellationToken);
    public IAsyncEnumerable<Space> GetAllSpaces(CancellationToken cancellationToken);
}