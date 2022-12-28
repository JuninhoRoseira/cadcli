using System.Linq.Expressions;

namespace Dominio.Interfaces.Dados
{
    public interface IRepository<TEntity> where TEntity : class
    {
        TEntity Create(TEntity entity);
        TEntity Update(TEntity entity);
        void Delete(TEntity entity);
        TEntity FindBy(Expression<Func<TEntity, bool>> predicate, string includes = null!);
        IEnumerable<TEntity> GetBy(Expression<Func<TEntity, bool>> predicate, string includes = null!);
    }
}
