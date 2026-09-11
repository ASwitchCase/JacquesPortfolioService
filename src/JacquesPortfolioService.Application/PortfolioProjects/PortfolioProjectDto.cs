using AutoMapper;

public record PortfolioProjectDto
{
    public Guid Id {get;init;}
    public String Title {get;init;}
    public String Spec {get;init;}
    public String Detail {get;init;}
    public List<Guid> SkillIds {get;init;}
    public DateTime CreatedAt {get;init;}
}
public class PortfolioProjectMappingProfile : Profile
{
    public PortfolioProjectMappingProfile()
    {
        CreateMap<PortfolioProject,PortfolioProjectDto>();
    }
}
