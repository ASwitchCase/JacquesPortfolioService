using MediatR;

public record DeleteWorkExperienceCommand(Guid experienceId) : IRequest;

public class DeleteWorkExperienceCommandHandler(IWorkExperienceRepository workExperienceRepository) : IRequestHandler<DeleteWorkExperienceCommand>
{
    public async Task Handle(DeleteWorkExperienceCommand request, CancellationToken ct)
    {
        var experience = await workExperienceRepository.GetByIdAsync(request.experienceId, ct);

        if (experience is null)
        {
            throw new KeyNotFoundException();
        }

        workExperienceRepository.Remove(experience);

        await workExperienceRepository.SaveChangesAsync(ct);
    }
}
