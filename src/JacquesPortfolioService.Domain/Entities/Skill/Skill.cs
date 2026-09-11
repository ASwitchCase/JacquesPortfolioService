public class Skill
{
    public Guid Id { get; private set; }
    public String Name { get; private set; }
    public DateTime CreatedAt { get; private set; }
    private readonly List<IDomainEvent> _domainEvents = new();
    public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();
    private Skill(){}
    public static  Skill Create(String skillName)
    {
        if (skillName.Length == 0)
        {
            throw new Exception("Please provide a valid name for a skill.");
        }
        var skill = new Skill
        {
            Id = Guid.NewGuid(),
            Name = skillName,
            CreatedAt = DateTime.Now
        };
        return skill;
    }
    public void ClearDomaminEvents() => _domainEvents.Clear();

    public void Rename(String newName)
    {
        if (newName.Length == 0)
        {
            throw new Exception("Please provide a valid name for a skill.");
        }
        Name = newName;
    }
}