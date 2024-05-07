using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebForum.Auth.Api.Authorization;
using WebForum.Auth.Domain.Models;
using WebForum.Core.Api.ExceptionFilters;
using WebForum.Core.Api.Requests;
using WebForum.Core.Api.Responses;
using WebForum.Core.Application.Commands;
using WebForum.Core.Application.Queries;

namespace WebForum.Core.Api.Controllers;

[ApiController]
[Route("api/post")]
[PostExceptionFilter]
public sealed class PostController(
    ISender sender
) : ControllerBase
{
    [HttpGet("{id:guid}")]
    public async Task<IResult> GetById(
        Guid id,
        CancellationToken cancellationToken
    )
    {
        var post = await sender.Send(new GetPostByIdQuery(id), cancellationToken);

        return Results.Ok(post);
    }

    [HttpGet]
    public async Task<IResult> GetAll(
        [FromQuery] int take,
        [FromQuery] int skip,
        CancellationToken cancellationToken)
    {
        var posts = await sender
            .CreateStream(new GetAllPostsQuery(take, skip), cancellationToken)
            .ToListAsync(cancellationToken);

        return Results.Ok(new GetAllPostsResponse(posts));
    }

    [HttpPost]
    [Authorization(Permissions.CanPublish)]
    public async Task<IResult> Create(
        CreatePostRequest request,
        CancellationToken cancellationToken
        )
    {
        var userId = (HttpContext.Items["UserId"] as Guid?)!.Value;
        
        await sender.Send(new CreatePostCommand(request.Content, request.ParentId, userId), cancellationToken);

        return Results.Ok();
    }
}