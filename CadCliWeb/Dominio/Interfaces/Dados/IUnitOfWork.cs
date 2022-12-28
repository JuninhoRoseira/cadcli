using Microsoft.EntityFrameworkCore;

namespace Dominio.Interfaces.Dados
{
    public interface IUnitOfWork<TContext> : IUnitOfWork where TContext : DbContext { }

    public interface IUnitOfWork : IDisposable
    {
        IRepository<TEntity> GetRepository<TEntity>() where TEntity : class;
        Task<int> SaveChanges();
    }

}
