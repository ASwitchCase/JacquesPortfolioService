public interface IEducationRepository
{
    Task<Education?> GetByIdAsync(Guid id, CancellationToken ct);
    Task<List<Education>> GetAllAsync(CancellationToken ct);
    void Add(Education education);
    void Remove(Education education);
    Task<bool> SaveChangesAsync(CancellationToken ct);
}
