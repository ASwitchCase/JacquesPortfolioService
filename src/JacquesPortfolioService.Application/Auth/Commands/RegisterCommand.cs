using MediatR;

public record RegisterCommand(string Email, string Password, string DisplayName) : IRequest<RegisterResponseDto?>;

public record RegisterResponseDto(Guid Id, string Email);

public class RegisterCommandHandler(IAuthService authService) : IRequestHandler<RegisterCommand, RegisterResponseDto?>
{
    public async Task<RegisterResponseDto?> Handle(RegisterCommand request, CancellationToken ct)
    {
        var result = await authService.RegisterAsync(request.Email, request.Password, request.DisplayName, ct);
        return result.Succeeded ? new RegisterResponseDto(result.UserId!.Value, result.Email!) : null;
    }
}
