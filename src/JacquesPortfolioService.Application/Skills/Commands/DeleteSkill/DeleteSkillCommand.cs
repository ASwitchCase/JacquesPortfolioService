using MediatR;

public record DeleteSkillCommand(Guid skillId) : IRequest;

public class DeleteSkillCommandHandler(ISkillRepository skillRepository) : IRequestHandler<DeleteSkillCommand>
{
    public async Task Handle(DeleteSkillCommand request, CancellationToken ct)
    {
        var skill = await skillRepository.GetByIdAsync(request.skillId, ct);

        if (skill is null)
        {
            throw new KeyNotFoundException();
        }

        skillRepository.Remove(skill);

        await skillRepository.SaveChangesAsync(ct);
    }
}
