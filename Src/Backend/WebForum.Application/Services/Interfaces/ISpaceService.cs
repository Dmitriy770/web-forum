using WebForum.Domain.Models;
using WebForum.Domain.Models.SpaceModels;

namespace WebForum.Application.Services.Interfaces;

public interface ISpaceService
{
    public Task<Space> GetById(Guid id, CancellationToken cancellationToken);
    public IAsyncEnumerable<Space> GetAll(CancellationToken cancellationToken);
}