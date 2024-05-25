using Domain.Event;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Mappings;

public sealed class DatesMapping : IEntityTypeConfiguration<Dates>

{
    public void Configure(EntityTypeBuilder<Dates> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id);

        builder.Property(e => e.Start)
            .IsRequired();

        builder.Property(e => e.End);

        builder.HasMany(e => e.Sectors)
            .WithOne(e => e.Dates)
            .HasPrincipalKey(e => e.Id)
            .HasForeignKey(e => e.DateId);
    }
}