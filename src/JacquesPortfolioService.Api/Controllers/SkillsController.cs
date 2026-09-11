using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/skills")]
[Authorize]
public class SkillsController(ISender sender) : ControllerBase
{
    [HttpGet("{id:guid}")]
    [ProducesResponseType<SkillDto>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<SkillDto>> GetById(Guid id, CancellationToken ct)
    {
        var result = await sender.Send(new GetSkillByIdQuery(id),ct);
        return Ok(result);
    }
    [HttpPost]
    [ProducesResponseType<SkillDto>(StatusCodes.Status201Created)]
    public async Task<ActionResult<SkillDto>> Create(CreateSkillCommand command, CancellationToken ct)
    {
        var result = await sender.Send(command, ct);
        return CreatedAtAction(nameof(GetById),new {id = result.Id},result);
    }

    [HttpGet]
    [ProducesResponseType<List<SkillDto>>(StatusCodes.Status200OK)]
    public async Task<ActionResult<List<SkillDto>>> List(CancellationToken ct)
    {
        var result = await sender.Send(new GetSkillsListQuery(), ct);
        return Ok(result);
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType<SkillDto>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<SkillDto>> Update(Guid id, UpdateSkillCommand command, CancellationToken ct)
    {
        var result = await sender.Send(command with { skillId = id }, ct);
        return Ok(result);
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
    {
        await sender.Send(new DeleteSkillCommand(id), ct);
        return NoContent();
    }

}