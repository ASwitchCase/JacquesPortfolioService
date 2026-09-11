using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class ContactConfiguration : IEntityTypeConfiguration<Contact>
{
    public void Configure(EntityTypeBuilder<Contact> builder)
    {
        builder.ToTable("Contacts");
        builder.HasKey(contact => contact.Id);

        builder.Property(contact => contact.Name)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(contact => contact.Email)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(contact => contact.Company)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(contact => contact.Message)
            .IsRequired();

        builder.Property(contact => contact.CreatedAt)
            .IsRequired();
    }
}
