using Core.Domain;

namespace Catalog.Domain.Organizer;

public sealed class Organizers : AggregateRoot
{
    public string Name { get; private set; }

    public static Organizers Create(Guid id, string name)
    {
        return new()
        {
            Id = id,
            Name = name,
        };
    }
}