using MediatR;
using WebForum.Core.Application.Interfaces;
using WebForum.Core.Domain.Exceptions;
using WebForum.Core.Domain.Models;

namespace WebForum.Core.Application.Queries;

public record GetProfileByUserIdQuery(
    Guid UserId
) : IRequest<Profile>;

internal sealed class GetProfileByUserIdQueryHandler(
    IProfileRepository profileRepository
) : IRequestHandler<GetProfileByUserIdQuery, Profile>
{
    public async Task<Profile> Handle(GetProfileByUserIdQuery request, CancellationToken cancellationToken)
    {
        var userId = request.UserId;

        var profile = await profileRepository.FindByUserId(userId, cancellationToken);
        if (profile is null)
        {
            throw new ProfileNotFoundException(userId);
        }

        return profile;
    }
}