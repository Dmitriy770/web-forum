using MediatR;
using WebForum.Auth.Api.Authorization;
using WebForum.Core.Api.Requests;
using WebForum.Core.Application.Commands;
using WebForum.Core.Application.Queries;
using WebForum.Core.Domain.Exceptions;
using WebForum.Core.Domain.Models;

namespace WebForum.Core.Api.Controllers;

public sealed class ProfileController
{
    public void Register(WebApplication app)
    {
        var group = app.MapGroup("/api/core/profile")
            .AddEndpointFilter<AuthorizationFilter>();

        group.MapPost("/{userId:guid}", CreateProfile)
            .Produces(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status409Conflict)
            .ProducesProblem(StatusCodes.Status500InternalServerError);

        group.MapGet("/{userId:guid}", GetProfileByUserId)
            .Produces<Profile>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesProblem(StatusCodes.Status401Unauthorized)
            .ProducesProblem(StatusCodes.Status500InternalServerError);

    }

    [NoAuthorization]
    private async Task<IResult> CreateProfile(
        Guid userId,
        CreateProfileRequest request,
        CancellationToken cancellationToken,
        ISender sender
    )
    {
        try
        {
            await sender.Send(new CreateProfileCommand(userId, request.DisplayName, request.AvatarUri), cancellationToken);
            return Results.Ok();
        }
        catch (Exception exception)
        {
            return HandleException(exception);
        }
    }

    private async Task<IResult> GetProfileByUserId(
        Guid userId,
        CancellationToken cancellationToken,
        ISender sender
        )
    {
        try
        {
            var profile = await sender.Send(new GetProfileByUserIdQuery(userId), cancellationToken);
            return Results.Ok(profile);
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
            ProfileNotFoundException => Results.NotFound(),
            ProfileAlreadyExistsException => Results.Conflict(),
            _ => Results.Problem(statusCode: StatusCodes.Status500InternalServerError)
        };
    }
}