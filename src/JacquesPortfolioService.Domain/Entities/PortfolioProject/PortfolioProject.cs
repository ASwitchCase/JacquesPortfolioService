public class PortfolioProject
{
    public Guid Id { get; private set; }
    public String Title { get; private set; }
    public String Spec { get; private set; }
    public String Detail { get; private set; }
    public List<Guid> SkillIds { get; private set; } = new();
    public DateTime CreatedAt { get; private set; }

    private PortfolioProject(){}

    public static PortfolioProject Create(String title, String spec, String detail, List<Guid> skillIds)
    {
        if (title.Length == 0)
        {
            throw new Exception("Please provide a valid title for a portfolio project.");
        }
        var project = new PortfolioProject
        {
            Id = Guid.NewGuid(),
            Title = title,
            Spec = spec,
            Detail = detail,
            SkillIds = skillIds ?? new(),
            CreatedAt = DateTime.Now
        };
        return project;
    }

    public void UpdateDetails(String title, String spec, String detail, List<Guid> skillIds)
    {
        if (title.Length == 0)
        {
            throw new Exception("Please provide a valid title for a portfolio project.");
        }
        Title = title;
        Spec = spec;
        Detail = detail;
        SkillIds = skillIds ?? new();
    }
}
