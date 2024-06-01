using Catalog.Domain.Organizer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Catalog.Infra.Mappings;

public sealed class OrganizersMapping : IEntityTypeConfiguration<Organizers>
{
    public void Configure(EntityTypeBuilder<Organizers> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id);

        builder.Property(e => e.Name);
    }
}