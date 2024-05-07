using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebForum.Auth.Api.Authorization;
using WebForum.Core.Api.ExceptionFilters;
using WebForum.Core.Api.Requests;
using WebForum.Core.Application.Commands;
using WebForum.Core.Application.Queries;

namespace WebForum.Core.Api.Controllers;

[ApiController]
[Route("api/profile")]
[ProfileExceptionFilter]
public class ProfileController(
    ISender sender
) : ControllerBase
{
    [HttpPost("{userId:guid}")]
    [NoAuthorization]
    public async Task<IResult> Create(
        Guid userId,
        CreateProfileRequest request,
        CancellationToken cancellationToken)
    {
        await sender.Send(new CreateProfileCommand(userId, request.DisplayName, request.AvatarUri), cancellationToken);

        return Results.Ok();
    }

    [HttpGet("{userId:guid}")]
    public async Task<IResult> GetByUserId(
        Guid userId,
        CancellationToken cancellationToken)
    {
        var profile = await sender.Send(new GetProfileByUserIdQuery(userId), cancellationToken);

        return Results.Ok(profile);
    }
}