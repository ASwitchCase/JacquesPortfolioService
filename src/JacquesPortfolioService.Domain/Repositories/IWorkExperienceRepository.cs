public interface IWorkExperienceRepository
{
    Task<WorkExperience?> GetByIdAsync(Guid id, CancellationToken ct);
    Task<List<WorkExperience>> GetAllAsync(CancellationToken ct);
    void Add(WorkExperience experience);
    void Remove(WorkExperience experience);
    Task<bool> SaveChangesAsync(CancellationToken ct);
}
