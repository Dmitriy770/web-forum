using MediatR;
using Microsoft.AspNetCore.OData.Deltas;
using WebForum.Application.Extensions;
using WebForum.Application.Interfaces;
using WebForum.Application.Services.Interfaces;
using WebForum.Domain.Models;
using WebForum.Domain.Models.SpaceModels;
using WebForum.Domain.Models.UserModels;

namespace WebForum.Application.Commands;

public record UpdateSpaceCommand(
    Guid SpaceId,
    Delta<Space> Delta,
    User User
)  : IRequest<Space>;

internal sealed class UpdateSpaceCommandHandler(
    ISpaceService spaceService,
    ISpaceRepository spaceRepository
) : IRequestHandler<UpdateSpaceCommand, Space>
{
    public async Task<Space> Handle(UpdateSpaceCommand request, CancellationToken cancellationToken)
    {
        var (spaceId, delta, user) = request;

        var space = await spaceService.GetById(spaceId, cancellationToken);
        if (space.Author.Id != user.Id)
        {
            throw new ArgumentException(nameof(user));
        }
        
        var newSpace = delta.Patch(space);
        await spaceRepository.Update(newSpace.ToEntity(), cancellationToken);

        return newSpace;
    }
}

