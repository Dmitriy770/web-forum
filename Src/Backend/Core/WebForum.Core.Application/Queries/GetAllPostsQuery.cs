using System.Runtime.CompilerServices;
using MediatR;
using WebForum.Core.Application.Interfaces;
using WebForum.Core.Domain.Exceptions;
using WebForum.Core.Domain.Models;

namespace WebForum.Core.Application.Queries;

public record GetAllPostsQuery(
    int Take,
    int Skip
) : IStreamRequest<Post>;

internal sealed class GetAllPostsQueryHandler(
    IPostRepository postRepository,
    IProfileRepository profileRepository
) : IStreamRequestHandler<GetAllPostsQuery, Post>
{
    public async IAsyncEnumerable<Post> Handle(GetAllPostsQuery request, [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        var (take, skip) = request;

        await foreach (var post in postRepository.GetAll(take, skip, cancellationToken))
        {
            var userId = post.Profile.UserId;
            var profile = await profileRepository.FindByUserId(userId, cancellationToken);
            if (profile is null)
            {
                throw new ProfileNotFoundException(userId);
            }

            if (post.IsVisible)
            {
                yield return post with { Profile = profile };
            }
            else
            {
                yield return post with { Profile = profile, Content = string.Empty };
            }
        }
    }
}