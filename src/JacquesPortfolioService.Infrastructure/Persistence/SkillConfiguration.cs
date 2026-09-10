using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class SkillConfiguration : IEntityTypeConfiguration<Skill>
{
    public void Configure(EntityTypeBuilder<Skill> builder)
    {
        builder.ToTable("Skills");
        builder.HasKey(skill => skill.Id);

        builder.Property(skill => skill.Name)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(skill => skill.CreatedAt)
            .IsRequired();
    }
}