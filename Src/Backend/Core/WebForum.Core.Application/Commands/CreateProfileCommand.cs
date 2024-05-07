using MediatR;
using WebForum.Core.Application.Interfaces;
using WebForum.Core.Domain.Exceptions;
using WebForum.Core.Domain.Models;

namespace WebForum.Core.Application.Commands;

public record CreateProfileCommand(
    Guid UserId,
    string DisplayName,
    Uri? AvatarUri
) : IRequest;

internal sealed class CreateProfileCommandHandler(
    IProfileRepository profileRepository
) : IRequestHandler<CreateProfileCommand>
{
    public async Task Handle(CreateProfileCommand request, CancellationToken cancellationToken)
    {
        var (userId, displayName, avatarUri) = request;

        var oldProfile = await profileRepository.FindByUserId(userId, cancellationToken);
        if (oldProfile is not null)
        {
            throw new ProfileAlreadyExistsException(userId);
        }

        var profile = new Profile
        {
            UserId = userId,
            DisplayName = displayName,
            AvatarUri = avatarUri
        };

        await profileRepository.Save(profile, cancellationToken);
    }
}