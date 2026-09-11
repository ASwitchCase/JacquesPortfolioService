using AutoMapper;

public record WorkExperienceDto
{
    public Guid Id {get;init;}
    public String Company {get;init;}
    public String Title {get;init;}
    public DateTime StartDate {get;init;}
    public DateTime? EndDate {get;init;}
    public String Description {get;init;}
    public String Location {get;init;}
    public DateTime CreatedAt {get;init;}
}
public class WorkExperienceMappingProfile : Profile
{
    public WorkExperienceMappingProfile()
    {
        CreateMap<WorkExperience,WorkExperienceDto>();
    }
}
