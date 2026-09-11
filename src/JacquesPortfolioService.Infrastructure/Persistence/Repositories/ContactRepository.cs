using Microsoft.EntityFrameworkCore;

public class ContactRepository(ApplicationDbContext db) : IContactRepository
{
    public void Add(Contact contact)
    {
        db.Contacts.Add(contact);
    }
    public void Remove(Contact contact)
    {
        db.Contacts.Remove(contact);
    }
    public async Task<Contact?> GetByIdAsync(Guid id, CancellationToken ct)
    {
        return await db.Contacts.FirstOrDefaultAsync(c => c.Id == id, ct);
    }

    public async Task<List<Contact>> GetAllAsync(CancellationToken ct)
    {
        return await db.Contacts.ToListAsync(ct);
    }

    public async Task<bool> SaveChangesAsync(CancellationToken ct)
    {
        return await db.SaveChangesAsync(ct) > 0;
    }
}
