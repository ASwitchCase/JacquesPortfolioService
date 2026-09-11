using Microsoft.EntityFrameworkCore;

public class EducationRepository(ApplicationDbContext db) : IEducationRepository
{
    public void Add(Education education)
    {
        db.Educations.Add(education);
    }
    public void Remove(Education education)
    {
        db.Educations.Remove(education);
    }
    public async Task<Education?> GetByIdAsync(Guid id, CancellationToken ct)
    {
        return await db.Educations.FirstOrDefaultAsync(e => e.Id == id, ct);
    }

    public async Task<List<Education>> GetAllAsync(CancellationToken ct)
    {
        return await db.Educations.ToListAsync(ct);
    }

    public async Task<bool> SaveChangesAsync(CancellationToken ct)
    {
        return await db.SaveChangesAsync(ct) > 0;
    }
}
