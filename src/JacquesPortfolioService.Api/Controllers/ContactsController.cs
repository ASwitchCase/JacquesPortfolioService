using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/contacts")]
[Authorize]
public class ContactsController(ISender sender) : ControllerBase
{
    [HttpGet("{id:guid}")]
    [ProducesResponseType<ContactDto>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ContactDto>> GetById(Guid id, CancellationToken ct)
    {
        var result = await sender.Send(new GetContactByIdQuery(id),ct);
        return Ok(result);
    }
    [HttpPost]
    [ProducesResponseType<ContactDto>(StatusCodes.Status201Created)]
    public async Task<ActionResult<ContactDto>> Create(CreateContactCommand command, CancellationToken ct)
    {
        var result = await sender.Send(command, ct);
        return CreatedAtAction(nameof(GetById),new {id = result.Id},result);
    }

    [HttpGet]
    [ProducesResponseType<List<ContactDto>>(StatusCodes.Status200OK)]
    public async Task<ActionResult<List<ContactDto>>> List(CancellationToken ct)
    {
        var result = await sender.Send(new GetContactsListQuery(), ct);
        return Ok(result);
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
    {
        await sender.Send(new DeleteContactCommand(id), ct);
        return NoContent();
    }

}
