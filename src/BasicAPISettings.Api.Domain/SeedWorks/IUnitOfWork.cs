namespace BasicAPISettings.Api.Domain.SeedWorks;
public interface IUnitOfWork
{
    Task<bool> Commit();
}
