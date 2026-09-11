using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/portfolioprojects")]
[Authorize]
public class PortfolioProjectsController(ISender sender) : ControllerBase
{
    [HttpGet("{id:guid}")]
    [ProducesResponseType<PortfolioProjectDto>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<PortfolioProjectDto>> GetById(Guid id, CancellationToken ct)
    {
        var result = await sender.Send(new GetPortfolioProjectByIdQuery(id),ct);
        return Ok(result);
    }
    [HttpPost]
    [ProducesResponseType<PortfolioProjectDto>(StatusCodes.Status201Created)]
    public async Task<ActionResult<PortfolioProjectDto>> Create(CreatePortfolioProjectCommand command, CancellationToken ct)
    {
        var result = await sender.Send(command, ct);
        return CreatedAtAction(nameof(GetById),new {id = result.Id},result);
    }

    [HttpGet]
    [ProducesResponseType<List<PortfolioProjectDto>>(StatusCodes.Status200OK)]
    public async Task<ActionResult<List<PortfolioProjectDto>>> List(CancellationToken ct)
    {
        var result = await sender.Send(new GetPortfolioProjectsListQuery(), ct);
        return Ok(result);
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType<PortfolioProjectDto>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<PortfolioProjectDto>> Update(Guid id, UpdatePortfolioProjectCommand command, CancellationToken ct)
    {
        var result = await sender.Send(command with { projectId = id }, ct);
        return Ok(result);
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
    {
        await sender.Send(new DeletePortfolioProjectCommand(id), ct);
        return NoContent();
    }

}
