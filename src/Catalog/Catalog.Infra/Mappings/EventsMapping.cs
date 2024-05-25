using Domain.Event;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Mappings
{
    public sealed class EventsMapping : IEntityTypeConfiguration<Events>
    {
        public void Configure(EntityTypeBuilder<Events> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id);

            builder.Property(e => e.Name)
                .HasMaxLength(1000)
                .IsRequired();

            builder.Property(e => e.Description)
                .HasMaxLength(4000);

            builder.Property(e => e.Image)
                .HasMaxLength(4000);

            builder.Property(e => e.OrganizerId)
                .IsRequired();

            builder.HasMany(e => e.Dates)
                .WithOne(e => e.Event)
                .HasPrincipalKey(e => e.Id)
                .HasForeignKey(e => e.EventId);

            builder.ComplexProperty(e => e.Address, addressBuilder =>
            {
                addressBuilder.Property(e => e.City)
                    .HasMaxLength(1000);
                addressBuilder.Property(e => e.Complement)
                    .HasMaxLength(1000);
                addressBuilder.Property(e => e.Country)
                    .HasMaxLength(1000);
                addressBuilder.Property(e => e.District)
                    .HasMaxLength(1000);
                addressBuilder.Property(e => e.State)
                    .HasMaxLength(1000);
                addressBuilder.Property(e => e.Street)
                    .HasMaxLength(2000);
            });

            builder.HasIndex(e => e.OrganizerId);
            builder.HasIndex(e => e.Name);
        }
    }
}