using AutoMapper;

public record ContactDto
{
    public Guid Id {get;init;}
    public String Name {get;init;}
    public String Email {get;init;}
    public String Company {get;init;}
    public String Message {get;init;}
    public DateTime CreatedAt {get;init;}
}
public class ContactMappingProfile : Profile
{
    public ContactMappingProfile()
    {
        CreateMap<Contact,ContactDto>();
    }
}
