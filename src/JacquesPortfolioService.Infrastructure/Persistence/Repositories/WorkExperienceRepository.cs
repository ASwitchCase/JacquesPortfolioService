using Microsoft.EntityFrameworkCore;

public class WorkExperienceRepository(ApplicationDbContext db) : IWorkExperienceRepository
{
    public void Add(WorkExperience experience)
    {
        db.WorkExperiences.Add(experience);
    }
    public void Remove(WorkExperience experience)
    {
        db.WorkExperiences.Remove(experience);
    }
    public async Task<WorkExperience?> GetByIdAsync(Guid id, CancellationToken ct)
    {
        return await db.WorkExperiences.FirstOrDefaultAsync(e => e.Id == id, ct);
    }

    public async Task<List<WorkExperience>> GetAllAsync(CancellationToken ct)
    {
        return await db.WorkExperiences.ToListAsync(ct);
    }

    public async Task<bool> SaveChangesAsync(CancellationToken ct)
    {
        return await db.SaveChangesAsync(ct) > 0;
    }
}
