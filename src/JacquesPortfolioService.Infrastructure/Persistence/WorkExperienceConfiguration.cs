using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class WorkExperienceConfiguration : IEntityTypeConfiguration<WorkExperience>
{
    public void Configure(EntityTypeBuilder<WorkExperience> builder)
    {
        builder.ToTable("WorkExperiences");
        builder.HasKey(experience => experience.Id);

        builder.Property(experience => experience.Company)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(experience => experience.Title)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(experience => experience.StartDate)
            .IsRequired();

        builder.Property(experience => experience.EndDate);

        builder.Property(experience => experience.Description)
            .IsRequired();

        builder.Property(experience => experience.Location)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(experience => experience.CreatedAt)
            .IsRequired();
    }
}
