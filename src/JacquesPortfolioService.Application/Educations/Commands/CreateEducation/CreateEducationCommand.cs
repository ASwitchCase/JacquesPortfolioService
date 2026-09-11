using AutoMapper;
using MediatR;

public record CreateEducationCommand(String institution, String degree, String fieldOfStudy, DateTime startDate, DateTime? endDate, String description) : IRequest<EducationDto>;

public class CreateEducationCommandHandler (
    IEducationRepository educationRepository,
    IMapper mapper
) : IRequestHandler<CreateEducationCommand, EducationDto>
{
    public async Task<EducationDto> Handle(CreateEducationCommand request, CancellationToken ct)
    {
        var education = Education.Create(request.institution, request.degree, request.fieldOfStudy, request.startDate, request.endDate, request.description);
        educationRepository.Add(education);

        await educationRepository.SaveChangesAsync(ct);

        return mapper.Map<EducationDto>(education);
    }
}
