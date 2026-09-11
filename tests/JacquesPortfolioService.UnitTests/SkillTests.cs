namespace JacquesPortfolioService.UnitTests;

public class SkillTests
{
    [Fact]
    public void Rename_WithValidName_UpdatesName()
    {
        var skill = Skill.Create("Original");

        skill.Rename("Updated");

        Assert.Equal("Updated", skill.Name);
    }

    [Fact]
    public void Rename_WithEmptyName_Throws()
    {
        var skill = Skill.Create("Original");

        Assert.Throws<Exception>(() => skill.Rename(""));
    }
}
