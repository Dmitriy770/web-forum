using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using WebForum.Api.Filters;
using WebForum.Application.Commands;
using WebForum.Application.Queries;
using WebForum.Domain.Models.UserModels;

namespace WebForum.Api.Controllers;

public class UserController(
    ISender sender
) : ODataController
{
    public async Task<ActionResult> Post(
        [FromBody] UserDraft userDraft,
        CancellationToken cancellationToken
    )
    {
        var user = await sender.Send(new CreateUserCommand(userDraft), cancellationToken);
        return Created(user);
    }

    [Authorization]
    public async Task<ActionResult<User>> Get(
        [FromRoute] Guid key,
        CancellationToken cancellationToken)
    {
        var user = await sender.Send(new GetUserByIdQuery(key), cancellationToken);

        return Ok(user);
    }

    [Authorization]
    public async Task<ActionResult> Patch(
        [FromRoute] Guid key,
        [FromBody] Delta<User> delta,
        CancellationToken cancellationToken)
    {
        var user = await sender.Send(new UpdateUserCommand(key, delta), cancellationToken);

        return Updated(user);
    }
}