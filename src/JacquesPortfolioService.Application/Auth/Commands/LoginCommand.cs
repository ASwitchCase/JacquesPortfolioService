using MediatR;

public record LoginCommand(string Email, string Password) : IRequest<AuthResponseDto?>;

public record AuthResponseDto(string AccessToken, DateTimeOffset ExpiresAtUtc);

public class LoginCommandHandler(IAuthService authService) : IRequestHandler<LoginCommand, AuthResponseDto?>
{
    public Task<AuthResponseDto?> Handle(LoginCommand request, CancellationToken ct)
        => authService.LoginAsync(request.Email, request.Password, ct);
}
