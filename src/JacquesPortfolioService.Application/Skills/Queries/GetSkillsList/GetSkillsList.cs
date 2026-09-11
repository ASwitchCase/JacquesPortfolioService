using AutoMapper;
using MediatR;

public record GetSkillsListQuery : IRequest<List<SkillDto>>;

public class GetSkillsListQueryHandler(ISkillRepository skillRepository, IMapper mapper)
: IRequestHandler<GetSkillsListQuery, List<SkillDto>>
{
    public async Task<List<SkillDto>> Handle(GetSkillsListQuery request, CancellationToken ct)
    {
        var skills = await skillRepository.GetAllAsync(ct);

        return mapper.Map<List<SkillDto>>(skills);
    }
}
