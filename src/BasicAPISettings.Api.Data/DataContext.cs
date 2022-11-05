using BasicAPISettings.Api.Domain.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Linq.Expressions;

namespace BasicAPISettings.Api.Data
{
    public class DataContext : DbContext, IRepository
    {
        private readonly ILoggerFactory _logger = new LoggerFactory();

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DataContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.ConfigureWarnings(c => c.Log((RelationalEventId.CommandExecuting, LogLevel.Debug)));
            optionsBuilder.UseLoggerFactory(_logger);
            optionsBuilder.EnableSensitiveDataLogging();
        }

        public async Task<bool> Commit() => await base.SaveChangesAsync() > 0;

        public IQueryable<T> Query<T>(params Expression<Func<T, object>>[] includeProperties) where T : class
        {
            var query = base.Set<T>().AsQueryable();

            if (includeProperties != null)
                query = includeProperties.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));

            return query;
        }

        public new async Task Add<T>(T entity) where T : class => await base.Set<T>().AddAsync(entity);

        public new void Update<T>(T entity) where T : class => base.Set<T>().Update(entity);

        public async Task AddRange<T>(T entity) where T : class => await base.Set<T>().AddRangeAsync(entity);

        public new void Remove<T>(T item) where T : class => base.Remove(item);

        public void RemoveRange<T>(T items) where T : class => base.RemoveRange(items);
    }
}