using MediatR;
using WebForum.Application.Extensions;
using WebForum.Application.Interfaces;
using WebForum.Domain.Models;

namespace WebForum.Application.Commands;

public record UpdateSpaceCommand(
    Space Space
)  : IRequest<Space>;

internal sealed class UpdateSpaceCommandHandler(
    ISpaceRepository spaceRepository
) : IRequestHandler<UpdateSpaceCommand, Space>
{
    public async Task<Space> Handle(UpdateSpaceCommand request, CancellationToken cancellationToken)
    {
        var space = request.Space;

        await spaceRepository.Update(space.ToEntity(), cancellationToken);

        return space;
    }
}

