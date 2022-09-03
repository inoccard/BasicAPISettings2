using BasicAPISettings.Api.Domain.SeedWorks;
using System.Linq.Expressions;

namespace BasicAPISettings.Api.Domain.Repository;

public interface IRepository : IUnitOfWork
{
    IQueryable<T> Query<T>(params Expression<Func<T, object>>[] includeProperties) where T : class;
    Task Add<T>(T entity) where T : class;
    void Remove<T>(T item) where T : class;
}
