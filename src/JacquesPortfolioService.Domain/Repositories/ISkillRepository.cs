public interface ISkillRepository
{
    Task<Skill?> GetByIdAsync(Guid id, CancellationToken ct);
    void Add(Skill skill);
    Task<bool> SaveChangesAsync(CancellationToken ct);
}