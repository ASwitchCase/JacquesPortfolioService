public class Contact
{
    public Guid Id { get; private set; }
    public String Name { get; private set; }
    public String Email { get; private set; }
    public String Company { get; private set; }
    public String Message { get; private set; }
    public DateTime CreatedAt { get; private set; }

    private Contact(){}

    public static Contact Create(String name, String email, String company, String message)
    {
        if (name.Length == 0)
        {
            throw new Exception("Please provide a valid name for a contact.");
        }
        if (email.Length == 0)
        {
            throw new Exception("Please provide a valid email for a contact.");
        }
        var contact = new Contact
        {
            Id = Guid.NewGuid(),
            Name = name,
            Email = email,
            Company = company,
            Message = message,
            CreatedAt = DateTime.Now
        };
        return contact;
    }
}
