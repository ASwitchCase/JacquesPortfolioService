public interface ISkillRepository
{
    Task<Skill?> GetByIdAsync(Guid id, CancellationToken ct);
    Task<List<Skill>> GetAllAsync(CancellationToken ct);
    void Add(Skill skill);
    void Remove(Skill skill);
    Task<bool> SaveChangesAsync(CancellationToken ct);
}