using AutoMapper;
using MediatR;

public record GetWorkExperienceByIdQuery(Guid experienceId) : IRequest<WorkExperienceDto>;

public class GetWorkExperienceByIdQueryHandler( IWorkExperienceRepository workExperienceRepository, IMapper mapper)
: IRequestHandler<GetWorkExperienceByIdQuery,WorkExperienceDto>
{
    public async Task<WorkExperienceDto> Handle(GetWorkExperienceByIdQuery request, CancellationToken ct)
    {
        var experience = await workExperienceRepository.GetByIdAsync(request.experienceId,ct);

        if(experience is null)
        {
            throw new KeyNotFoundException();
        }

        return mapper.Map<WorkExperienceDto>(experience);
    }
}
