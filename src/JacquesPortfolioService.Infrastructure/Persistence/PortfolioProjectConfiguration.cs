using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class PortfolioProjectConfiguration : IEntityTypeConfiguration<PortfolioProject>
{
    public void Configure(EntityTypeBuilder<PortfolioProject> builder)
    {
        builder.ToTable("PortfolioProjects");
        builder.HasKey(project => project.Id);

        builder.Property(project => project.Title)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(project => project.Spec)
            .HasMaxLength(500)
            .IsRequired();

        builder.Property(project => project.Detail)
            .IsRequired();

        builder.Property(project => project.CreatedAt)
            .IsRequired();
    }
}
