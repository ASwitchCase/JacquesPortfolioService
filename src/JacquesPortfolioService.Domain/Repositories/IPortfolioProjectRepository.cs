public interface IPortfolioProjectRepository
{
    Task<PortfolioProject?> GetByIdAsync(Guid id, CancellationToken ct);
    Task<List<PortfolioProject>> GetAllAsync(CancellationToken ct);
    void Add(PortfolioProject project);
    void Remove(PortfolioProject project);
    Task<bool> SaveChangesAsync(CancellationToken ct);
}
