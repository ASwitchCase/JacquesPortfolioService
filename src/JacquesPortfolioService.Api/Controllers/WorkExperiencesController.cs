using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/workexperiences")]
[Authorize]
public class WorkExperiencesController(ISender sender) : ControllerBase
{
    [HttpGet("{id:guid}")]
    [ProducesResponseType<WorkExperienceDto>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<WorkExperienceDto>> GetById(Guid id, CancellationToken ct)
    {
        var result = await sender.Send(new GetWorkExperienceByIdQuery(id),ct);
        return Ok(result);
    }
    [HttpPost]
    [ProducesResponseType<WorkExperienceDto>(StatusCodes.Status201Created)]
    public async Task<ActionResult<WorkExperienceDto>> Create(CreateWorkExperienceCommand command, CancellationToken ct)
    {
        var result = await sender.Send(command, ct);
        return CreatedAtAction(nameof(GetById),new {id = result.Id},result);
    }

    [HttpGet]
    [ProducesResponseType<List<WorkExperienceDto>>(StatusCodes.Status200OK)]
    public async Task<ActionResult<List<WorkExperienceDto>>> List(CancellationToken ct)
    {
        var result = await sender.Send(new GetWorkExperiencesListQuery(), ct);
        return Ok(result);
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType<WorkExperienceDto>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<WorkExperienceDto>> Update(Guid id, UpdateWorkExperienceCommand command, CancellationToken ct)
    {
        var result = await sender.Send(command with { experienceId = id }, ct);
        return Ok(result);
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
    {
        await sender.Send(new DeleteWorkExperienceCommand(id), ct);
        return NoContent();
    }

}
