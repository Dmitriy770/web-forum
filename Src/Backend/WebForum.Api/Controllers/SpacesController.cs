using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using WebForum.Api.Filters;
using WebForum.Application.Commands;
using WebForum.Application.Queries;
using WebForum.Domain.Models.AuthModels;
using WebForum.Domain.Models.SpaceModels;
using WebForum.Domain.Models.UserModels;

namespace WebForum.Api.Controllers;

public class SpacesController(
    ISender sender
) : ODataController
{
    [EnableQuery]
    [Authorization]
    public async Task<ActionResult<IEnumerable<Space>>> Get(
        CancellationToken cancellationToken)
    {
        var spaces = await sender
            .CreateStream(new GetAllSpaces(), cancellationToken)
            .ToListAsync(cancellationToken);
        
        return Ok(spaces);
    }

    [EnableQuery]
    [Authorization]
    public async Task<ActionResult<Space>> Get(
        [FromRoute] Guid key,
        CancellationToken cancellationToken)
    {
        var space = await sender.Send(new GetSpaceByIdQuery(key), cancellationToken);
        
        return Ok(space);
    }

    [Authorization]
    public async Task<ActionResult> Post(
        [FromBody] SpaceDraft spaceDraft,
        CancellationToken cancellationToken)
    {
        var user = (HttpContext.Items[nameof(User)] as User)!;
        var space = await sender.Send(new CreateSpaceCommand(spaceDraft,  user), cancellationToken);

        return Created(space);
    }

    [Authorization]
    public async Task<IActionResult> Patch(
        Guid key,
        [FromBody] Delta<Space> delta,
        CancellationToken cancellationToken)
    {
        var user = (HttpContext.Items[nameof(User)] as User)!;
        var space = await sender.Send(new UpdateSpaceCommand(key, delta, user), cancellationToken);

        return Updated(space);
    }
}