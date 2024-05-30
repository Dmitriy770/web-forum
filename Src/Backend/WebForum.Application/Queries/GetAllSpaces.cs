using MediatR;
using WebForum.Application.Services.Interfaces;
using WebForum.Domain.Models;
using WebForum.Domain.Models.SpaceModels;

namespace WebForum.Application.Queries;

public record GetAllSpaces() : IStreamRequest<Space>;

internal sealed class GetAllSpacesHandler(
    ISpaceService spaceService
) : IStreamRequestHandler<GetAllSpaces, Space>
{
    public IAsyncEnumerable<Space> Handle(GetAllSpaces request, CancellationToken cancellationToken)
    {
        return spaceService.GetAll(cancellationToken);
    }
}