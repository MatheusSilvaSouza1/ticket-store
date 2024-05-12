using Domain.Event;
using Domain.Organizer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Mappings
{
    public class EventsMapping : IEntityTypeConfiguration<Events>
    {
        public void Configure(EntityTypeBuilder<Events> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id);

            builder.Property(e => e.Name)
                .HasMaxLength(1000);

            builder.Property(e => e.Description);

            builder.Property(e => e.Image);

            builder.Property(e => e.OrganizerId);

            builder.Property(e => e.IsPublished)
                .HasDefaultValue(false);

            builder.HasOne<Organizers>()
                .WithMany()
                .HasPrincipalKey(organizers => organizers.Id)
                .HasForeignKey(events => events.OrganizerId);

            builder.HasMany(e => e.Sectors)
                .WithOne(e => e.Event)
                .HasPrincipalKey(e => e.Id)
                .HasForeignKey(e => e.EventId);

            builder.ComplexProperty(e => e.Address);

            builder.ComplexProperty(e => e.DateRange);

            builder.HasIndex(e => e.OrganizerId);
            builder.HasIndex(e => e.Name);
        }
    }
}