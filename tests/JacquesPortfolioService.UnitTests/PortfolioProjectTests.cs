namespace JacquesPortfolioService.UnitTests;

public class PortfolioProjectTests
{
    [Fact]
    public void Create_WithValidData_SetsProperties()
    {
        var skillIds = new List<Guid> { Guid.NewGuid() };

        var project = PortfolioProject.Create("Title", "Spec", "Detail", skillIds);

        Assert.Equal("Title", project.Title);
        Assert.Equal("Spec", project.Spec);
        Assert.Equal("Detail", project.Detail);
        Assert.Equal(skillIds, project.SkillIds);
    }

    [Fact]
    public void Create_WithEmptyTitle_Throws()
    {
        Assert.Throws<Exception>(() => PortfolioProject.Create("", "Spec", "Detail", new List<Guid>()));
    }

    [Fact]
    public void UpdateDetails_WithValidData_UpdatesProperties()
    {
        var project = PortfolioProject.Create("Title", "Spec", "Detail", new List<Guid>());
        var newSkillIds = new List<Guid> { Guid.NewGuid() };

        project.UpdateDetails("New Title", "New Spec", "New Detail", newSkillIds);

        Assert.Equal("New Title", project.Title);
        Assert.Equal("New Spec", project.Spec);
        Assert.Equal("New Detail", project.Detail);
        Assert.Equal(newSkillIds, project.SkillIds);
    }

    [Fact]
    public void UpdateDetails_WithEmptyTitle_Throws()
    {
        var project = PortfolioProject.Create("Title", "Spec", "Detail", new List<Guid>());

        Assert.Throws<Exception>(() => project.UpdateDetails("", "Spec", "Detail", new List<Guid>()));
    }
}
