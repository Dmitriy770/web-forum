using MediatR;
using WebForum.Core.Application.Interfaces;
using WebForum.Core.Domain.Exceptions;
using WebForum.Core.Domain.Models;

namespace WebForum.Core.Application.Commands;

public record CreatePostCommand(
    string PostContent,
    Guid? ParentId,
    Guid UserId
) : IRequest;

internal sealed class CreatePostCommandHandler(
    IDataTimeProvider dataTimeProvider,
    IProfileRepository profileRepository,
    IPostRepository postRepository
) : IRequestHandler<CreatePostCommand>
{
    public async Task Handle(CreatePostCommand request, CancellationToken cancellationToken)
    {
        var (postContent, parentId, userId) = request;

        var profile = await profileRepository.FindByUserId(userId, cancellationToken);
        if (profile is null)
        {
            throw new ProfileNotFoundException(userId);
        }

        var id = Guid.NewGuid();
        var creationDate = dataTimeProvider.Now();

        var post = new Post
        {
            Id = id,
            ParentId = parentId,
            Content = postContent,
            CreationDate = creationDate,
            IsVisible = true,
            Profile = profile
        };

        await postRepository.Save(post, cancellationToken);
    }
}