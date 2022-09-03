namespace BasicAPISettings.Api.Domain.SeedWorks;

public abstract class EntityId<T>
{
    public virtual T Id { get; set; }

    public abstract bool IsUnassigned();

    public override bool Equals(object obj)
    {
        var compareTo = obj as EntityId<T>;

        if (ReferenceEquals(this, compareTo)) return true;
        return compareTo is not null && Id.Equals(compareTo.Id);
    }

    public override int GetHashCode() => (GetType().GetHashCode() * 907) + Id.GetHashCode();

    public override string ToString() => $"{GetType().Name} [Id={Id}]";
}

