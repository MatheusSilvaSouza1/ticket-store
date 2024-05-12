using Domain.Event;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Mappings;

public class SectorsMapping : IEntityTypeConfiguration<Sectors>
{
    public void Configure(EntityTypeBuilder<Sectors> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.PlaceName);

        builder.Property(e => e.NumberOfSeats);
    }
}