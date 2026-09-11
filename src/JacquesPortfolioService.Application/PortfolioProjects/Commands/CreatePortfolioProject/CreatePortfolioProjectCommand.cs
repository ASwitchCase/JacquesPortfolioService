using AutoMapper;
using MediatR;

public record CreatePortfolioProjectCommand(String title, String spec, String detail, List<Guid> skillIds) : IRequest<PortfolioProjectDto>;

public class CreatePortfolioProjectCommandHandler (
    IPortfolioProjectRepository portfolioProjectRepository,
    IMapper mapper
) : IRequestHandler<CreatePortfolioProjectCommand, PortfolioProjectDto>
{
    public async Task<PortfolioProjectDto> Handle(CreatePortfolioProjectCommand request, CancellationToken ct)
    {
        var project = PortfolioProject.Create(request.title, request.spec, request.detail, request.skillIds);
        portfolioProjectRepository.Add(project);

        await portfolioProjectRepository.SaveChangesAsync(ct);

        return mapper.Map<PortfolioProjectDto>(project);
    }
}
