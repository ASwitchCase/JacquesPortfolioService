using AutoMapper;
using MediatR;

public record UpdatePortfolioProjectCommand(Guid projectId, String title, String spec, String detail, List<Guid> skillIds) : IRequest<PortfolioProjectDto>;

public class UpdatePortfolioProjectCommandHandler(
    IPortfolioProjectRepository portfolioProjectRepository,
    IMapper mapper
) : IRequestHandler<UpdatePortfolioProjectCommand, PortfolioProjectDto>
{
    public async Task<PortfolioProjectDto> Handle(UpdatePortfolioProjectCommand request, CancellationToken ct)
    {
        var project = await portfolioProjectRepository.GetByIdAsync(request.projectId, ct);

        if (project is null)
        {
            throw new KeyNotFoundException();
        }

        project.UpdateDetails(request.title, request.spec, request.detail, request.skillIds);

        await portfolioProjectRepository.SaveChangesAsync(ct);

        return mapper.Map<PortfolioProjectDto>(project);
    }
}
