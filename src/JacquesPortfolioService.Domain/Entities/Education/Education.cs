public class Education
{
    public Guid Id { get; private set; }
    public String Institution { get; private set; }
    public String Degree { get; private set; }
    public String FieldOfStudy { get; private set; }
    public DateTime StartDate { get; private set; }
    public DateTime? EndDate { get; private set; }
    public String Description { get; private set; }
    public DateTime CreatedAt { get; private set; }

    private Education(){}

    public static Education Create(String institution, String degree, String fieldOfStudy, DateTime startDate, DateTime? endDate, String description)
    {
        if (institution.Length == 0)
        {
            throw new Exception("Please provide a valid institution for an education entry.");
        }
        if (degree.Length == 0)
        {
            throw new Exception("Please provide a valid degree for an education entry.");
        }
        if (fieldOfStudy.Length == 0)
        {
            throw new Exception("Please provide a valid field of study for an education entry.");
        }
        var education = new Education
        {
            Id = Guid.NewGuid(),
            Institution = institution,
            Degree = degree,
            FieldOfStudy = fieldOfStudy,
            StartDate = startDate,
            EndDate = endDate,
            Description = description,
            CreatedAt = DateTime.Now
        };
        return education;
    }

    public void UpdateDetails(String institution, String degree, String fieldOfStudy, DateTime startDate, DateTime? endDate, String description)
    {
        if (institution.Length == 0)
        {
            throw new Exception("Please provide a valid institution for an education entry.");
        }
        if (degree.Length == 0)
        {
            throw new Exception("Please provide a valid degree for an education entry.");
        }
        if (fieldOfStudy.Length == 0)
        {
            throw new Exception("Please provide a valid field of study for an education entry.");
        }
        Institution = institution;
        Degree = degree;
        FieldOfStudy = fieldOfStudy;
        StartDate = startDate;
        EndDate = endDate;
        Description = description;
    }
}
