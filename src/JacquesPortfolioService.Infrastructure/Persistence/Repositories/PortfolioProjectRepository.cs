using Microsoft.EntityFrameworkCore;

public class PortfolioProjectRepository(ApplicationDbContext db) : IPortfolioProjectRepository
{
    public void Add(PortfolioProject project)
    {
        db.PortfolioProjects.Add(project);
    }
    public void Remove(PortfolioProject project)
    {
        db.PortfolioProjects.Remove(project);
    }
    public async Task<PortfolioProject?> GetByIdAsync(Guid id, CancellationToken ct)
    {
        return await db.PortfolioProjects.FirstOrDefaultAsync(p => p.Id == id, ct);
    }

    public async Task<List<PortfolioProject>> GetAllAsync(CancellationToken ct)
    {
        return await db.PortfolioProjects.ToListAsync(ct);
    }

    public async Task<bool> SaveChangesAsync(CancellationToken ct)
    {
        return await db.SaveChangesAsync(ct) > 0;
    }
}
