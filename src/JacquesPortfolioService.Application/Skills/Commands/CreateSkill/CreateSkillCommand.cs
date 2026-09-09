using AutoMapper;
using MediatR;

public record CreateSkillCommand(String skillName) : IRequest<SkillDto>;

public class CreateSkillCommandHandler (
    ISkillRepository skillRepository,
    IMapper mapper
) : IRequestHandler<CreateSkillCommand, SkillDto>
{
    public async Task<SkillDto> Handle(CreateSkillCommand request, CancellationToken ct)
    {
        var skill = Skill.Create(request.skillName);
        skillRepository.Add(skill);

        await skillRepository.SaveChangesAsync(ct);

        return mapper.Map<SkillDto>(skill);
    }
}