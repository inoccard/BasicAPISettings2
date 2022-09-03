namespace BasicAPISettings.Api.Domain.SeedWorks;

public abstract class EntityGuidId : EntityId<Guid>
{
    public override bool IsUnassigned() => Id == Guid.Empty;
}
