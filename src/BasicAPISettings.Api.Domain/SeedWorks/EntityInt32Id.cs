namespace BasicAPISettings.Api.Domain.SeedWorks;

public abstract class EntityInt32Id : EntityId<int>
{
    public override bool IsUnassigned() => Id == 0;
}
