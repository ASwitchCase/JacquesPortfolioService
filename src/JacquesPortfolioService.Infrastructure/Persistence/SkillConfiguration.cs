using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class SkillConfiguration : IEntityTypeConfiguration<Skill>
{
    public void Configure(EntityTypeBuilder<Skill> builder)
    {
        builder.ToContainer("skills");
        builder.HasKey(skill => skill.Id);
        builder.HasPartitionKey(skill => skill.Id);
    }
}