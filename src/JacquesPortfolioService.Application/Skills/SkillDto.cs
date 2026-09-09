using AutoMapper;

public record SkillDto
{
    public Guid Id {get;init;}
    public String Name {get;init;}
    public DateTime CreatedAt {get;init;}
}
public class SkillMappingProfile : Profile
{
    public SkillMappingProfile()
    {
        CreateMap<Skill,SkillDto>();
    }
}