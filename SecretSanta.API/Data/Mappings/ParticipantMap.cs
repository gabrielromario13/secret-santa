using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SecretSanta.API.Domain.Models;

namespace SecretSanta.API.Data.Mappings;

public class ParticipantMap : IEntityTypeConfiguration<Participant>
{
    public void Configure(EntityTypeBuilder<Participant> builder)
    {
        builder.ToTable("Participants");

        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(100);
        
        builder.Property(x => x.Email)
            .IsRequired()
            .HasMaxLength(200)
            .IsUnicode();
        
        builder.Property(x => x.Password)
            .IsRequired()
            .HasMaxLength(100);
    }
}