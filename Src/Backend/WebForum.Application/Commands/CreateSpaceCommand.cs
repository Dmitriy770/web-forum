using MediatR;
using WebForum.Application.Extensions;
using WebForum.Application.Interfaces;
using WebForum.Domain.Models;
using WebForum.Domain.Models.SpaceModels;
using WebForum.Domain.Models.UserModels;

namespace WebForum.Application.Commands;

public record CreateSpaceCommand(
    SpaceDraft Draft,
    User Author
) : IRequest<Space>;

internal sealed class CreateSpaceCommandHandler(
    ISpaceRepository spaceRepository
) : IRequestHandler<CreateSpaceCommand, Space>
{
    public async Task<Space> Handle(CreateSpaceCommand request, CancellationToken cancellationToken)
    {
        var (draft, author) = request;

        var space = new Space
        {
            Id = Guid.NewGuid(),
            Type = draft.Type,
            Name = draft.Name,
            Description = draft.Description,
            CreationDate = DateTime.UtcNow,
            Author = author,
            Tags = new List<Tag>()
        };

        await spaceRepository.Add(space.ToEntity(), cancellationToken);

        return space;
    }
}