using MediatR;
using WebForum.Application.Services.Interfaces;
using WebForum.Domain.Models;

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

        var space = await spaceService.GetSpace(id, cancellationToken);

        return space;
    }
}