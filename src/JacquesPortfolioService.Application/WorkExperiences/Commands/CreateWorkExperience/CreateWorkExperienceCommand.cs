using AutoMapper;
using MediatR;

public record CreateWorkExperienceCommand(String company, String title, DateTime startDate, DateTime? endDate, String description, String location) : IRequest<WorkExperienceDto>;

public class CreateWorkExperienceCommandHandler (
    IWorkExperienceRepository workExperienceRepository,
    IMapper mapper
) : IRequestHandler<CreateWorkExperienceCommand, WorkExperienceDto>
{
    public async Task<WorkExperienceDto> Handle(CreateWorkExperienceCommand request, CancellationToken ct)
    {
        var experience = WorkExperience.Create(request.company, request.title, request.startDate, request.endDate, request.description, request.location);
        workExperienceRepository.Add(experience);

        await workExperienceRepository.SaveChangesAsync(ct);

        return mapper.Map<WorkExperienceDto>(experience);
    }
}
