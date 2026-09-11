using AutoMapper;
using MediatR;

public record GetPortfolioProjectByIdQuery(Guid projectId) : IRequest<PortfolioProjectDto>;

public class GetPortfolioProjectByIdQueryHandler( IPortfolioProjectRepository portfolioProjectRepository, IMapper mapper)
: IRequestHandler<GetPortfolioProjectByIdQuery,PortfolioProjectDto>
{
    public async Task<PortfolioProjectDto> Handle(GetPortfolioProjectByIdQuery request, CancellationToken ct)
    {
        var project = await portfolioProjectRepository.GetByIdAsync(request.projectId,ct);

        if(project is null)
        {
            throw new KeyNotFoundException();
        }

        return mapper.Map<PortfolioProjectDto>(project);
    }
}
