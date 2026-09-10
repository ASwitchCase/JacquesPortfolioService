using MediatR;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/skills")]
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

}