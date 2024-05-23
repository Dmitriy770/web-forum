using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebForum.Auth.Api.Authorization;
using WebForum.Auth.Domain.Models;
using WebForum.Core.Api.Requests;
using WebForum.Core.Api.Responses;
using WebForum.Core.Application.Commands;
using WebForum.Core.Application.Queries;
using WebForum.Core.Domain.Exceptions;
using WebForum.Core.Domain.Models;

namespace WebForum.Core.Api.Controllers;

public class PostController
{
    public void Register(WebApplication app)
    {
        var group = app.MapGroup("/api/post")
            .AddEndpointFilter<AuthorizationFilter>();

        group.MapGet("/{id:guid}", GetPostById)
            .Produces<Post>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesProblem(StatusCodes.Status401Unauthorized)
            .ProducesProblem(StatusCodes.Status500InternalServerError);

        group.MapGet("/", GetAllPosts)
            .Produces<List<Post>>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status401Unauthorized)
            .ProducesProblem(StatusCodes.Status500InternalServerError);

        group.MapPost("/", CreatePost)
            .Produces(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status401Unauthorized)
            .ProducesProblem(StatusCodes.Status403Forbidden)
            .ProducesProblem(StatusCodes.Status500InternalServerError);
    }

    private async Task<IResult> GetPostById(
        Guid id,
        CancellationToken cancellationToken,
        ISender sender)
    {
        try
        {
            var post = await sender.Send(new GetPostByIdQuery(id), cancellationToken);
            return Results.Ok(post);
        }
        catch (Exception exception)
        {
            return HandleException(exception);
        }
    }

    private async Task<IResult> GetAllPosts(
        [FromQuery] int take,
        [FromQuery] int skip,
        CancellationToken cancellationToken,
        ISender sender)
    {
        try
        {
            var posts = await sender
                .CreateStream(new GetAllPostsQuery(take, skip), cancellationToken)
                .ToListAsync(cancellationToken);
            return Results.Ok(new GetAllPostsResponse(posts));
        }
        catch (Exception exception)
        {
            return HandleException(exception);
        }
    }

    [Authorization(Permissions.CanPublish)]
    private async Task<IResult> CreatePost(
        HttpContext context,
        CreatePostRequest request,
        CancellationToken cancellationToken,
        ISender sender)
    {
        try
        {
            var userId = (context.Items["UserId"] as Guid?)!.Value;
            await sender.Send(new CreatePostCommand(request.Content, request.ParentId, userId), cancellationToken);
            return Results.Ok();
        }
        catch (Exception exception)
        {
            return HandleException(exception);
        }
    }

    private IResult HandleException(Exception exception)
    {
        return exception switch
        {
            PostNotFoundException => Results.StatusCode(statusCode: StatusCodes.Status404NotFound),
            ProfileNotFoundException => Results.Problem(statusCode: StatusCodes.Status500InternalServerError),
            _ => Results.Problem(statusCode: StatusCodes.Status500InternalServerError)
        };
    }
}