using Microsoft.EntityFrameworkCore;

public interface IApplicationDbContext
{
    DbSet<Skill> Skills {get;}
    DbSet<PortfolioProject> PortfolioProjects {get;}
    DbSet<WorkExperience> WorkExperiences {get;}
    DbSet<Education> Educations {get;}
    DbSet<Contact> Contacts {get;}
}