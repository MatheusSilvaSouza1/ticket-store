using Domain.Organizer;
using Domain.Organizer.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Mappings;

public class OrganizersMapping : IEntityTypeConfiguration<Organizers>
{
    public void Configure(EntityTypeBuilder<Organizers> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id);

        builder.Property(e => e.CorporateReason);

        builder.Property(e => e.Fantasy);

        builder.Property(e => e.Cnpj)
            .IsRequired()
            .HasConversion(e => e!.Value, e => Cnpj.Create(e).Value);

        // builder.OwnsOne(e => e.Cnpj);

        builder.HasIndex(e => e.Cnpj);
    }
}