using MediatR;
using WebForum.Auth.Api.Authorization;
using WebForum.Auth.Api.Requests;
using WebForum.Auth.Api.Responses;
using WebForum.Auth.Application.Commands;
using WebForum.Auth.Domain.Exceptions;

namespace WebForum.Auth.Api.Controllers;

public sealed class UserController
{
    public void Register(WebApplication app)
    {
        var group = app.MapGroup("/api/auth/user")
            .AddEndpointFilter<AuthorizationFilter>();

        group.MapPost("/", CreateUser)
            .Produces<CreateUserResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status500InternalServerError);
        group.MapDelete("/", DeleteUser)
            .Produces(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status401Unauthorized)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesProblem(StatusCodes.Status409Conflict)
            .ProducesProblem(StatusCodes.Status500InternalServerError);
    }

    [NoAuthorization]
    private async Task<IResult> CreateUser(
        CreateUserRequest request,
        CancellationToken cancellationToken,
        ISender sender)
    {
        try
        {
            var userId = await sender.Send(new CreateUserCommand(request.Login, request.Password), cancellationToken);
            return Results.Ok(new CreateUserResponse(userId));
        }
        catch (Exception exception)
        {
            return HandleException(exception);
        }
    }

    private async Task<IResult> DeleteUser(
        Guid id,
        CancellationToken cancellationToken,
        ISender sender)
    {
        try
        {
            await sender.Send(new DeleteUserCommand(id), cancellationToken);
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
            UserAlreadyExistsException => Results.Problem(statusCode: StatusCodes.Status409Conflict),
            UserNotFoundException => Results.Problem(statusCode: StatusCodes.Status404NotFound),
            _ => Results.Problem(statusCode: StatusCodes.Status500InternalServerError)
        };
    }
}