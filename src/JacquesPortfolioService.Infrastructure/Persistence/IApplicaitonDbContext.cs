using Microsoft.EntityFrameworkCore;

public interface IApplicationDbContext
{
    DbSet<Skill> Skills {get;}
    DbSet<PortfolioProject> PortfolioProjects {get;}
}