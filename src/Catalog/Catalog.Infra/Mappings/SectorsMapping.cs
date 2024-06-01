using Domain.Event;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Mappings;

public sealed class SectorsMapping : IEntityTypeConfiguration<Sectors>
{
    public void Configure(EntityTypeBuilder<Sectors> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id);

        builder.Property(e => e.PlaceName)
            .HasMaxLength(1000);

        builder.Property(e => e.NumberOfSeats);
    }
}