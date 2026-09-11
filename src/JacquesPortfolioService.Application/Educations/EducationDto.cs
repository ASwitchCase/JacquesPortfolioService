using AutoMapper;

public record EducationDto
{
    public Guid Id {get;init;}
    public String Institution {get;init;}
    public String Degree {get;init;}
    public String FieldOfStudy {get;init;}
    public DateTime StartDate {get;init;}
    public DateTime? EndDate {get;init;}
    public String Description {get;init;}
    public DateTime CreatedAt {get;init;}
}
public class EducationMappingProfile : Profile
{
    public EducationMappingProfile()
    {
        CreateMap<Education,EducationDto>();
    }
}
