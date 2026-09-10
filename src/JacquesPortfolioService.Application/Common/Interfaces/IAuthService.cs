public record RegisterResult(bool Succeeded, Guid? UserId, string? Email, IReadOnlyList<string> Errors);

// Implements a narrow interface Application defines, the same pattern as ITokenGenerator —
// keeps UserManager/RoleManager (Identity-specific) entirely out of Application.
public interface IAuthService
{
    Task<AuthResponseDto?> LoginAsync(string email, string password, CancellationToken ct);
    Task<RegisterResult> RegisterAsync(string email, string password, string displayName, CancellationToken ct);
}
