using AutoMapper;
using MediatR;

public record GetPortfolioProjectsListQuery : IRequest<List<PortfolioProjectDto>>;

public class GetPortfolioProjectsListQueryHandler(IPortfolioProjectRepository portfolioProjectRepository, IMapper mapper)
: IRequestHandler<GetPortfolioProjectsListQuery, List<PortfolioProjectDto>>
{
    public async Task<List<PortfolioProjectDto>> Handle(GetPortfolioProjectsListQuery request, CancellationToken ct)
    {
        var projects = await portfolioProjectRepository.GetAllAsync(ct);

        return mapper.Map<List<PortfolioProjectDto>>(projects);
    }
}
