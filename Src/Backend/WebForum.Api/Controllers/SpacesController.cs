using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using WebForum.Domain.Models;

namespace WebForum.Api.Controllers;

public class SpacesController : ODataController
{
    private static readonly List<Space> _spaces = [new Space{Id = Guid.NewGuid()}, new Space{Id = Guid.NewGuid()}];

    [EnableQuery]
    public ActionResult<IEnumerable<Space>> Get()
    {
        return Ok(_spaces);
    }

    [EnableQuery]
    public ActionResult<Space> Get([FromRoute] Guid key)
    {
        var result = _spaces.FirstOrDefault(s => s.Id == key);
        return Ok(result);
    }

    public ActionResult Post([FromBody]SpaceDraft spaceDraft)
    {
        // _spaces.Add(space);

        return Created(spaceDraft);
    }

    public IActionResult Patch(Guid key, [FromBody] Delta<Space> delta)
    {
        
    }
}