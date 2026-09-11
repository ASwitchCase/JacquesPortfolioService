public interface IContactRepository
{
    Task<Contact?> GetByIdAsync(Guid id, CancellationToken ct);
    Task<List<Contact>> GetAllAsync(CancellationToken ct);
    void Add(Contact contact);
    void Remove(Contact contact);
    Task<bool> SaveChangesAsync(CancellationToken ct);
}
