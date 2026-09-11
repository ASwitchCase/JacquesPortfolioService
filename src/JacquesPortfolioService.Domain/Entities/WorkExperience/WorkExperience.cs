public class WorkExperience
{
    public Guid Id { get; private set; }
    public String Company { get; private set; }
    public String Title { get; private set; }
    public DateTime StartDate { get; private set; }
    public DateTime? EndDate { get; private set; }
    public String Description { get; private set; }
    public String Location { get; private set; }
    public DateTime CreatedAt { get; private set; }

    private WorkExperience(){}

    public static WorkExperience Create(String company, String title, DateTime startDate, DateTime? endDate, String description, String location)
    {
        if (company.Length == 0)
        {
            throw new Exception("Please provide a valid company for a work experience.");
        }
        if (title.Length == 0)
        {
            throw new Exception("Please provide a valid title for a work experience.");
        }
        var experience = new WorkExperience
        {
            Id = Guid.NewGuid(),
            Company = company,
            Title = title,
            StartDate = startDate,
            EndDate = endDate,
            Description = description,
            Location = location,
            CreatedAt = DateTime.Now
        };
        return experience;
    }

    public void UpdateDetails(String company, String title, DateTime startDate, DateTime? endDate, String description, String location)
    {
        if (company.Length == 0)
        {
            throw new Exception("Please provide a valid company for a work experience.");
        }
        if (title.Length == 0)
        {
            throw new Exception("Please provide a valid title for a work experience.");
        }
        Company = company;
        Title = title;
        StartDate = startDate;
        EndDate = endDate;
        Description = description;
        Location = location;
    }
}
