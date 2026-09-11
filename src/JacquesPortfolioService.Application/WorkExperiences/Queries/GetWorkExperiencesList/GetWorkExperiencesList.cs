using AutoMapper;
using MediatR;

public record GetWorkExperiencesListQuery : IRequest<List<WorkExperienceDto>>;

public class GetWorkExperiencesListQueryHandler(IWorkExperienceRepository workExperienceRepository, IMapper mapper)
: IRequestHandler<GetWorkExperiencesListQuery, List<WorkExperienceDto>>
{
    public async Task<List<WorkExperienceDto>> Handle(GetWorkExperiencesListQuery request, CancellationToken ct)
    {
        var experiences = await workExperienceRepository.GetAllAsync(ct);

        return mapper.Map<List<WorkExperienceDto>>(experiences);
    }
}
