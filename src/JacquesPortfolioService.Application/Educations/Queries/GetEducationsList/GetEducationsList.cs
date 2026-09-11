using AutoMapper;
using MediatR;

public record GetEducationsListQuery : IRequest<List<EducationDto>>;

public class GetEducationsListQueryHandler(IEducationRepository educationRepository, IMapper mapper)
: IRequestHandler<GetEducationsListQuery, List<EducationDto>>
{
    public async Task<List<EducationDto>> Handle(GetEducationsListQuery request, CancellationToken ct)
    {
        var educations = await educationRepository.GetAllAsync(ct);

        return mapper.Map<List<EducationDto>>(educations);
    }
}
