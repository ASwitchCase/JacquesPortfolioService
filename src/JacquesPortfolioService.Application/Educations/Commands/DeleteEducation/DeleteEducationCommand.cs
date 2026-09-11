using MediatR;

public record DeleteEducationCommand(Guid educationId) : IRequest;

public class DeleteEducationCommandHandler(IEducationRepository educationRepository) : IRequestHandler<DeleteEducationCommand>
{
    public async Task Handle(DeleteEducationCommand request, CancellationToken ct)
    {
        var education = await educationRepository.GetByIdAsync(request.educationId, ct);

        if (education is null)
        {
            throw new KeyNotFoundException();
        }

        educationRepository.Remove(education);

        await educationRepository.SaveChangesAsync(ct);
    }
}
