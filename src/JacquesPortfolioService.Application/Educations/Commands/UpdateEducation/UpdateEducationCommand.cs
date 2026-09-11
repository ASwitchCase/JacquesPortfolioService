using AutoMapper;
using MediatR;

public record UpdateEducationCommand(Guid educationId, String institution, String degree, String fieldOfStudy, DateTime startDate, DateTime? endDate, String description) : IRequest<EducationDto>;

public class UpdateEducationCommandHandler(
    IEducationRepository educationRepository,
    IMapper mapper
) : IRequestHandler<UpdateEducationCommand, EducationDto>
{
    public async Task<EducationDto> Handle(UpdateEducationCommand request, CancellationToken ct)
    {
        var education = await educationRepository.GetByIdAsync(request.educationId, ct);

        if (education is null)
        {
            throw new KeyNotFoundException();
        }

        education.UpdateDetails(request.institution, request.degree, request.fieldOfStudy, request.startDate, request.endDate, request.description);

        await educationRepository.SaveChangesAsync(ct);

        return mapper.Map<EducationDto>(education);
    }
}
