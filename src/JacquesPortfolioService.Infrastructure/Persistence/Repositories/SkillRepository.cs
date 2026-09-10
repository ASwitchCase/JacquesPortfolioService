using Microsoft.EntityFrameworkCore;

public class SkillRepository(ApplicationDbContext db) : ISkillRepository
{
    public void Add(Skill skill)
    {
        db.Skills.Add(skill);
    }
    public async Task<Skill?> GetByIdAsync(Guid id, CancellationToken ct)
    {
        return await db.Skills.FirstOrDefaultAsync(s => s.Id == id);
    }

    public async Task<bool> SaveChangesAsync(CancellationToken ct)
    {
        return await db.SaveChangesAsync(ct) > 0;
    }
}