using Dominio.Interfaces.Dados;
using Microsoft.EntityFrameworkCore;

namespace Infra.Dados
{
    public class UnitOfWork<TContext> : IUnitOfWork<TContext> where TContext : DbContext
    {
        private bool _disposed = false;

        private readonly Dictionary<Type, object> _repositories;
        private readonly TContext _context;

        public UnitOfWork(TContext context)
        {
            _context = context;
            _context.Database.SetCommandTimeout(120);

            _repositories = new Dictionary<Type, object>();

        }

        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            if (disposing)
            {
                _context?.Dispose();
            }

            _disposed = true;

        }

        public IRepository<TEntity> GetRepository<TEntity>() where TEntity : class
        {
            var type = typeof(TEntity);

            if (!_repositories.ContainsKey(type))
            {
                _repositories.Add(type, new Repository<TEntity>(_context));
            }

            return (IRepository<TEntity>)_repositories[type];

        }

        public async Task<int> SaveChanges()
        {
            var rowsAffected = await _context.SaveChangesAsync();

            DetachAllEntities();

            return rowsAffected;

        }

        private void DetachAllEntities()
        {
            var entries = _context.ChangeTracker
                .Entries()
                .Where(e => e.State != EntityState.Detached)
                .ToList();

            foreach (var entry in entries)
            {
                if (entry.Entity == null)
                    continue;

                entry.State = EntityState.Detached;

            }

        }

    }

}