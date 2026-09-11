using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/auth")]
public class AuthController(ISender sender) : ControllerBase
{
    [HttpPost("register")]
    [ProducesResponseType<RegisterResponseDto>(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult<RegisterResponseDto>> Register(RegisterCommand command, CancellationToken ct)
    {
        var result = await sender.Send(command, ct);
        if (!result.Succeeded)
        {
            if (result.RegistrationClosed)
                return StatusCode(StatusCodes.Status403Forbidden, new { errors = result.Errors });

            return result.EmailAlreadyExists
                ? Conflict(new { errors = result.Errors })
                : BadRequest(new { errors = result.Errors });
        }

        return StatusCode(
            StatusCodes.Status201Created,
            new RegisterResponseDto(result.UserId!.Value, result.Email!));
    }

    [HttpPost("login")]
    [ProducesResponseType<AuthResponseDto>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<AuthResponseDto>> Login(LoginCommand command, CancellationToken ct)
    {
        var result = await sender.Send(command, ct);
        return result is null ? Unauthorized() : Ok(result);
    }

    [HttpGet("me")]
    [Authorize]
    public ActionResult Me() => Ok(User.Claims.Select(c => new { c.Type, c.Value }));
}
