using MediatR;

public record RegisterCommand(string Email, string Password, string DisplayName) : IRequest<RegisterResult>;

public record RegisterResponseDto(Guid Id, string Email);

public class RegisterCommandHandler(IAuthService authService) : IRequestHandler<RegisterCommand, RegisterResult>
{
    public Task<RegisterResult> Handle(RegisterCommand request, CancellationToken ct)
    {
        return authService.RegisterAsync(request.Email, request.Password, request.DisplayName, ct);
    }
}
