using System.Runtime.CompilerServices;
using WebForum.Application.Entities;
using WebForum.Application.Interfaces;
using WebForum.Application.Services.Interfaces;
using WebForum.Domain.Models;
using WebForum.Domain.Models.SpaceModels;

namespace WebForum.Application.Services;

public class SpaceService(
    ISpaceRepository spaceRepository,
    IUserRepository userRepository
) : ISpaceService
{
    public async Task<Space> GetById(Guid id, CancellationToken cancellationToken)
    {
        var spaceEntity = await spaceRepository.GetById(id, cancellationToken);

        return await PrepareSpace(spaceEntity, cancellationToken);
    }

    public async IAsyncEnumerable<Space> GetAll([EnumeratorCancellation] CancellationToken cancellationToken)
    {
        await foreach (var spaceEntity in spaceRepository.GetAll(cancellationToken))
        {
            yield return await PrepareSpace(spaceEntity, cancellationToken);
        }
    }

    private async Task<Space> PrepareSpace(SpaceEntity spaceEntity, CancellationToken cancellationToken)
    {
        var author = await userRepository.GetById(spaceEntity.AuthorId, cancellationToken);
        
        var space = new Space
        {
            Id = spaceEntity.Id,
            Type = spaceEntity.Type,
            Name = spaceEntity.Name,
            Description = spaceEntity.Description,
            CreationDate = spaceEntity.CreationDate,
            Author = author,
            Tags = new List<Tag>()
        };

        return space;
    }
}