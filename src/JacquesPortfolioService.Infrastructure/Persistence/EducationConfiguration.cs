using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class EducationConfiguration : IEntityTypeConfiguration<Education>
{
    public void Configure(EntityTypeBuilder<Education> builder)
    {
        builder.ToTable("Educations");
        builder.HasKey(education => education.Id);

        builder.Property(education => education.Institution)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(education => education.Degree)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(education => education.FieldOfStudy)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(education => education.StartDate)
            .IsRequired();

        builder.Property(education => education.EndDate);

        builder.Property(education => education.Description)
            .IsRequired();

        builder.Property(education => education.CreatedAt)
            .IsRequired();
    }
}
