using AutoMapper;
using MediatR;

public record UpdateWorkExperienceCommand(Guid experienceId, String company, String title, DateTime startDate, DateTime? endDate, String description, String location) : IRequest<WorkExperienceDto>;

public class UpdateWorkExperienceCommandHandler(
    IWorkExperienceRepository workExperienceRepository,
    IMapper mapper
) : IRequestHandler<UpdateWorkExperienceCommand, WorkExperienceDto>
{
    public async Task<WorkExperienceDto> Handle(UpdateWorkExperienceCommand request, CancellationToken ct)
    {
        var experience = await workExperienceRepository.GetByIdAsync(request.experienceId, ct);

        if (experience is null)
        {
            throw new KeyNotFoundException();
        }

        experience.UpdateDetails(request.company, request.title, request.startDate, request.endDate, request.description, request.location);

        await workExperienceRepository.SaveChangesAsync(ct);

        return mapper.Map<WorkExperienceDto>(experience);
    }
}
