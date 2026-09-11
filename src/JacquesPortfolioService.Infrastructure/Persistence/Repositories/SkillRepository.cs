using Microsoft.EntityFrameworkCore;

public class SkillRepository(ApplicationDbContext db) : ISkillRepository
{
    public void Add(Skill skill)
    {
        db.Skills.Add(skill);
    }
    public void Remove(Skill skill)
    {
        db.Skills.Remove(skill);
    }
    public async Task<Skill?> GetByIdAsync(Guid id, CancellationToken ct)
    {
        return await db.Skills.FirstOrDefaultAsync(s => s.Id == id, ct);
    }

    public async Task<List<Skill>> GetAllAsync(CancellationToken ct)
    {
        return await db.Skills.ToListAsync(ct);
    }

    public async Task<bool> SaveChangesAsync(CancellationToken ct)
    {
        return await db.SaveChangesAsync(ct) > 0;
    }
}