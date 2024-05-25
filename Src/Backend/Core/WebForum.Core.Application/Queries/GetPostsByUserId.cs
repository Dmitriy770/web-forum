﻿using System.Runtime.CompilerServices;
using MediatR;
using WebForum.Core.Application.Interfaces;
using WebForum.Core.Domain.Exceptions;
using WebForum.Core.Domain.Models;

namespace WebForum.Core.Application.Queries;

public record GetPostsByUserId(
    Guid UserId,
    int Take,
    int Skip
) : IStreamRequest<Post>;

internal sealed class GetPostsByUserIdHandler(
    IPostRepository postRepository,
    IProfileRepository profileRepository
) : IStreamRequestHandler<GetPostsByUserId, Post>
{
    public async IAsyncEnumerable<Post> Handle(GetPostsByUserId request, [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        var (userId, take, skip) = request;
        
        await foreach (var post in postRepository.FindByUserId(userId, take, skip, cancellationToken))
        {
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