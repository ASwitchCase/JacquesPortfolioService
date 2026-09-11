using AutoMapper;
using MediatR;

public record UpdateSkillCommand(Guid skillId, String skillName) : IRequest<SkillDto>;

public class UpdateSkillCommandHandler(
    ISkillRepository skillRepository,
    IMapper mapper
) : IRequestHandler<UpdateSkillCommand, SkillDto>
{
    public async Task<SkillDto> Handle(UpdateSkillCommand request, CancellationToken ct)
    {
        var skill = await skillRepository.GetByIdAsync(request.skillId, ct);

        if (skill is null)
        {
            throw new KeyNotFoundException();
        }

        skill.Rename(request.skillName);

        await skillRepository.SaveChangesAsync(ct);

        return mapper.Map<SkillDto>(skill);
    }
}
