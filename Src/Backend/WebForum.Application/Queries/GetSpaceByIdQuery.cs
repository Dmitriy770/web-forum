using MediatR;
using WebForum.Application.Services.Interfaces;
using WebForum.Domain.Models;
using WebForum.Domain.Models.SpaceModels;

namespace WebForum.Application.Queries;

public record GetSpaceByIdQuery(
    Guid Id
) : IRequest<Space>;

internal sealed class GetSpaceByIdQueryHandler(
    ISpaceService spaceService
) : IRequestHandler<GetSpaceByIdQuery, Space>
{
    public async Task<Space> Handle(GetSpaceByIdQuery request, CancellationToken cancellationToken)
    {
        var id = request.Id;

        var space = await spaceService.GetById(id, cancellationToken);

        return space;
    }
}