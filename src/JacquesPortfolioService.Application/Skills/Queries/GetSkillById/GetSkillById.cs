using AutoMapper;
using MediatR;

public record GetSkillByIdQuery(Guid skillId) : IRequest<SkillDto>;

public class GetSkillByIdQueryHandler( ISkillRepository skillRepository, IMapper mapper)
: IRequestHandler<GetSkillByIdQuery,SkillDto>
{
    public async Task<SkillDto> Handle(GetSkillByIdQuery request, CancellationToken ct)
    {
        var skill = await skillRepository.GetByIdAsync(request.skillId,ct);

        if(skill is null)
        {
            throw new KeyNotFoundException();
        }

        return mapper.Map<SkillDto>(skill);
    }
}