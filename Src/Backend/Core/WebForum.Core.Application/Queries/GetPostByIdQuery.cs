using MediatR;
using WebForum.Core.Application.Interfaces;
using WebForum.Core.Domain.Exceptions;
using WebForum.Core.Domain.Models;

namespace WebForum.Core.Application.Queries;

public record GetPostByIdQuery(
    Guid PostId
) : IRequest<Post>;

internal sealed class GetPostByIdQueryHandler(
    IPostRepository postRepository,
    IProfileRepository profileRepository
) : IRequestHandler<GetPostByIdQuery, Post>
{
    public async Task<Post> Handle(GetPostByIdQuery request, CancellationToken cancellationToken)
    {
        var postId = request.PostId;

        var post = await postRepository.FindById(postId, cancellationToken);
        if (post is null)
        {
            throw new PostNotFoundException(postId);
        }

        var authorId = post.Profile.UserId;
        var profile = await profileRepository.FindByUserId(authorId, cancellationToken);
        if (profile is null)
        {
            throw new ProfileNotFoundException(authorId);
        }

        if (post.IsVisible)
        {
            return post with { Profile = profile };
        }
        else
        {
            return post with { Profile = profile, Content = string.Empty };
        }
    }
}