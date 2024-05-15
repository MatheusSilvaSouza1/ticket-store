namespace Core.Domain;


public abstract class Entity
{
    private Guid _id;

    public Guid Id
    {
        get
        {
            return _id;
        }
        protected set => _id = value;
    }

    public DateTime CreatedAt { get; protected set; } = DateTime.UtcNow.AddHours(-3);
    public DateTime? UpdateAt { get; protected set; }

    public bool IsTransient()
    {
        return Id == default;
    }

    public override bool Equals(object? obj)
    {
        if (obj is null or not Entity)
        {
            return false;
        }

        if (ReferenceEquals(this, obj))
        {
            return true;
        }

        if (GetType() != obj.GetType())
        {
            return false;
        }

        Entity item = (Entity)obj;

        if (item.IsTransient() || IsTransient())
        {
            return false;
        }
        else
        {
            return item.Id == Id;
        }
    }

    public static bool operator ==(Entity left, Entity right)
    {
        if (Equals(left, null))
        {
            return Equals(right, null);
        }
        else
        {
            return left.Equals(right);
        }
    }

    public static bool operator !=(Entity left, Entity right)
    {
        return !(left == right);
    }

    public override int GetHashCode() => _id.GetHashCode();
}
