namespace JacquesPortfolioService.UnitTests;

using AutoMapper;
using Microsoft.Extensions.Logging.Abstractions;

public class FakePortfolioProjectRepository : IPortfolioProjectRepository
{
    private readonly List<PortfolioProject> _projects = new();

    public void Add(PortfolioProject project) => _projects.Add(project);

    public void Remove(PortfolioProject project) => _projects.Remove(project);

    public Task<PortfolioProject?> GetByIdAsync(Guid id, CancellationToken ct)
        => Task.FromResult(_projects.FirstOrDefault(p => p.Id == id));

    public Task<List<PortfolioProject>> GetAllAsync(CancellationToken ct)
        => Task.FromResult(_projects.ToList());

    public Task<bool> SaveChangesAsync(CancellationToken ct) => Task.FromResult(true);
}

public class PortfolioProjectHandlerTests
{
    private static IMapper CreateMapper()
    {
        var config = new MapperConfiguration(cfg => cfg.AddProfile<PortfolioProjectMappingProfile>(), NullLoggerFactory.Instance);
        return config.CreateMapper();
    }

    [Fact]
    public async Task CreatePortfolioProjectCommandHandler_WithValidData_ReturnsMappedDtoAndPersists()
    {
        var repository = new FakePortfolioProjectRepository();
        var handler = new CreatePortfolioProjectCommandHandler(repository, CreateMapper());
        var skillIds = new List<Guid> { Guid.NewGuid() };

        var result = await handler.Handle(new CreatePortfolioProjectCommand("Title", "Spec", "Detail", skillIds), CancellationToken.None);

        Assert.Equal("Title", result.Title);
        Assert.Equal(skillIds, result.SkillIds);
        var stored = await repository.GetByIdAsync(result.Id, CancellationToken.None);
        Assert.NotNull(stored);
    }

    [Fact]
    public async Task UpdatePortfolioProjectCommandHandler_WithExistingProject_UpdatesProject()
    {
        var repository = new FakePortfolioProjectRepository();
        var project = PortfolioProject.Create("Title", "Spec", "Detail", new List<Guid>());
        repository.Add(project);
        var handler = new UpdatePortfolioProjectCommandHandler(repository, CreateMapper());

        var result = await handler.Handle(
            new UpdatePortfolioProjectCommand(project.Id, "New Title", "New Spec", "New Detail", new List<Guid>()),
            CancellationToken.None);

        Assert.Equal("New Title", result.Title);
        Assert.Equal("New Spec", result.Spec);
        Assert.Equal("New Detail", result.Detail);
    }

    [Fact]
    public async Task UpdatePortfolioProjectCommandHandler_WithUnknownId_ThrowsKeyNotFound()
    {
        var repository = new FakePortfolioProjectRepository();
        var handler = new UpdatePortfolioProjectCommandHandler(repository, CreateMapper());

        await Assert.ThrowsAsync<KeyNotFoundException>(() => handler.Handle(
            new UpdatePortfolioProjectCommand(Guid.NewGuid(), "Title", "Spec", "Detail", new List<Guid>()),
            CancellationToken.None));
    }

    [Fact]
    public async Task DeletePortfolioProjectCommandHandler_WithExistingProject_RemovesProject()
    {
        var repository = new FakePortfolioProjectRepository();
        var project = PortfolioProject.Create("Title", "Spec", "Detail", new List<Guid>());
        repository.Add(project);
        var handler = new DeletePortfolioProjectCommandHandler(repository);

        await handler.Handle(new DeletePortfolioProjectCommand(project.Id), CancellationToken.None);

        Assert.Null(await repository.GetByIdAsync(project.Id, CancellationToken.None));
    }

    [Fact]
    public async Task DeletePortfolioProjectCommandHandler_WithUnknownId_ThrowsKeyNotFound()
    {
        var repository = new FakePortfolioProjectRepository();
        var handler = new DeletePortfolioProjectCommandHandler(repository);

        await Assert.ThrowsAsync<KeyNotFoundException>(() =>
            handler.Handle(new DeletePortfolioProjectCommand(Guid.NewGuid()), CancellationToken.None));
    }

    [Fact]
    public async Task GetPortfolioProjectByIdQueryHandler_WithExistingProject_ReturnsMappedDto()
    {
        var repository = new FakePortfolioProjectRepository();
        var project = PortfolioProject.Create("Title", "Spec", "Detail", new List<Guid>());
        repository.Add(project);
        var handler = new GetPortfolioProjectByIdQueryHandler(repository, CreateMapper());

        var result = await handler.Handle(new GetPortfolioProjectByIdQuery(project.Id), CancellationToken.None);

        Assert.Equal(project.Id, result.Id);
        Assert.Equal("Title", result.Title);
    }

    [Fact]
    public async Task GetPortfolioProjectByIdQueryHandler_WithUnknownId_ThrowsKeyNotFound()
    {
        var repository = new FakePortfolioProjectRepository();
        var handler = new GetPortfolioProjectByIdQueryHandler(repository, CreateMapper());

        await Assert.ThrowsAsync<KeyNotFoundException>(() =>
            handler.Handle(new GetPortfolioProjectByIdQuery(Guid.NewGuid()), CancellationToken.None));
    }

    [Fact]
    public async Task GetPortfolioProjectsListQueryHandler_ReturnsAllProjectsMapped()
    {
        var repository = new FakePortfolioProjectRepository();
        repository.Add(PortfolioProject.Create("Title1", "Spec1", "Detail1", new List<Guid>()));
        repository.Add(PortfolioProject.Create("Title2", "Spec2", "Detail2", new List<Guid>()));
        var handler = new GetPortfolioProjectsListQueryHandler(repository, CreateMapper());

        var result = await handler.Handle(new GetPortfolioProjectsListQuery(), CancellationToken.None);

        Assert.Equal(2, result.Count);
    }
}
