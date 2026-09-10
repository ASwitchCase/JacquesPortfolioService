public interface ITokenGenerator
{
    string GenerateToken(Guid userId, string email, IList<string> roles);
}
