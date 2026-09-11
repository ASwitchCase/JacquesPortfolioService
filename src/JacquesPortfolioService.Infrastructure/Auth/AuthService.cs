using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

public class AuthService(
    UserManager<ApplicationUser> userManager,
    RoleManager<IdentityRole<Guid>> roleManager,
    ITokenGenerator tokenGenerator,
    IOptions<JwtSettings> jwtOptions) : IAuthService
{
    private const string BootstrapAdminRole = "Admin";

    public async Task<AuthResponseDto?> LoginAsync(string email, string password, CancellationToken ct)
    {
        var user = await userManager.FindByEmailAsync(email);
        if (user is null || !await userManager.CheckPasswordAsync(user, password))
            return null;

        var roles = await userManager.GetRolesAsync(user);
        var token = tokenGenerator.GenerateToken(user.Id, user.Email!, roles);

        return new AuthResponseDto(token, DateTimeOffset.UtcNow.AddMinutes(jwtOptions.Value.ExpiryMinutes));
    }

    public async Task<RegisterResult> RegisterAsync(string email, string password, string displayName, CancellationToken ct)
    {
        if (await userManager.Users.AnyAsync(ct))
            return new RegisterResult(
                false,
                null,
                null,
                ["Registration is closed. Contact the administrator."],
                RegistrationClosed: true);

        var existing = await userManager.FindByEmailAsync(email);
        if (existing is not null)
            return new RegisterResult(
                false,
                null,
                null,
                ["A user with this email already exists."],
                EmailAlreadyExists: true);

        var user = new ApplicationUser
        {
            Id = Guid.NewGuid(),
            UserName = email,
            Email = email,
            DisplayName = displayName,
        };

        var createResult = await userManager.CreateAsync(user, password);
        if (!createResult.Succeeded)
            return new RegisterResult(false, null, null, createResult.Errors.Select(e => e.Description).ToList());

        if (!await roleManager.RoleExistsAsync(BootstrapAdminRole))
            await roleManager.CreateAsync(new IdentityRole<Guid>(BootstrapAdminRole));
        await userManager.AddToRoleAsync(user, BootstrapAdminRole);

        return new RegisterResult(true, user.Id, user.Email, []);
    }
}
