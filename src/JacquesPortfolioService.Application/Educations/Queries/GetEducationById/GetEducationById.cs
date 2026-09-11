using AutoMapper;
using MediatR;

public record GetEducationByIdQuery(Guid educationId) : IRequest<EducationDto>;

public class GetEducationByIdQueryHandler( IEducationRepository educationRepository, IMapper mapper)
: IRequestHandler<GetEducationByIdQuery,EducationDto>
{
    public async Task<EducationDto> Handle(GetEducationByIdQuery request, CancellationToken ct)
    {
        var education = await educationRepository.GetByIdAsync(request.educationId,ct);

        if(education is null)
        {
            throw new KeyNotFoundException();
        }

        return mapper.Map<EducationDto>(education);
    }
}
