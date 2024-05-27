using MediatR;
using WebForum.Core.Application.Interfaces;
using WebForum.Core.Domain.Exceptions;

namespace WebForum.Core.Application.Commands;

public record ChangeVisibleCommand(
    Guid PostId,
    bool IsVisible
) : IRequest;

internal sealed class ChangeVisibleCommandHandler(
    IPostRepository postRepository
) : IRequestHandler<ChangeVisibleCommand>
{
    public async Task Handle(ChangeVisibleCommand request, CancellationToken cancellationToken)
    {
        var (postId, isVisible) = request;

        var oldPost = await postRepository.FindById(postId, cancellationToken);
        if (oldPost is null)
        {
            throw new PostNotFoundException(postId);
        }

        var newPost = oldPost with { IsVisible = isVisible };
        await postRepository.Update(newPost, cancellationToken);
    }
}