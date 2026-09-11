using MediatR;

public record DeletePortfolioProjectCommand(Guid projectId) : IRequest;

public class DeletePortfolioProjectCommandHandler(IPortfolioProjectRepository portfolioProjectRepository) : IRequestHandler<DeletePortfolioProjectCommand>
{
    public async Task Handle(DeletePortfolioProjectCommand request, CancellationToken ct)
    {
        var project = await portfolioProjectRepository.GetByIdAsync(request.projectId, ct);

        if (project is null)
        {
            throw new KeyNotFoundException();
        }

        portfolioProjectRepository.Remove(project);

        await portfolioProjectRepository.SaveChangesAsync(ct);
    }
}
