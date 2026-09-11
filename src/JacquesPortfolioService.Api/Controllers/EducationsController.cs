using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/educations")]
[Authorize]
public class EducationsController(ISender sender) : ControllerBase
{
    [HttpGet("{id:guid}")]
    [ProducesResponseType<EducationDto>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<EducationDto>> GetById(Guid id, CancellationToken ct)
    {
        var result = await sender.Send(new GetEducationByIdQuery(id),ct);
        return Ok(result);
    }
    [HttpPost]
    [ProducesResponseType<EducationDto>(StatusCodes.Status201Created)]
    public async Task<ActionResult<EducationDto>> Create(CreateEducationCommand command, CancellationToken ct)
    {
        var result = await sender.Send(command, ct);
        return CreatedAtAction(nameof(GetById),new {id = result.Id},result);
    }

    [HttpGet]
    [ProducesResponseType<List<EducationDto>>(StatusCodes.Status200OK)]
    public async Task<ActionResult<List<EducationDto>>> List(CancellationToken ct)
    {
        var result = await sender.Send(new GetEducationsListQuery(), ct);
        return Ok(result);
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType<EducationDto>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<EducationDto>> Update(Guid id, UpdateEducationCommand command, CancellationToken ct)
    {
        var result = await sender.Send(command with { educationId = id }, ct);
        return Ok(result);
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
    {
        await sender.Send(new DeleteEducationCommand(id), ct);
        return NoContent();
    }

}
