using Dominio.Interfaces.Dados;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infra.Dados
{

    /*
        sudo docker pull mcr.microsoft.com/mssql/server:2022-latest

        sudo docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=Pontepret@01" -p 1433:1433 --name sql1 --hostname sql1 -d mcr.microsoft.com/mssql/server:2022-latest
     
     
     */
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly DbContext _context;
        private readonly DbSet<TEntity> _dbSet;

        public Repository(DbContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }

        public TEntity Create(TEntity entity)
        {
            _dbSet.Add(entity);

            return entity;

        }

        public TEntity Update(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Modified;

            return entity;

        }

        public void Delete(TEntity entity)
        {
            _dbSet.Remove(entity);
        }

        public TEntity FindBy(Expression<Func<TEntity, bool>> predicate, string includes = null!)
        {
            var query = CreateQuery(predicate, includes);

            return query.FirstOrDefault();

        }

        public IEnumerable<TEntity> GetBy(Expression<Func<TEntity, bool>> predicate, string includes = null!)
        {
            var query = CreateQuery(predicate, includes);

            return query.ToList();

        }

        private IQueryable<TEntity> CreateQuery(Expression<Func<TEntity, bool>> predicate, string includes)
        {
            var query = _dbSet.AsNoTracking();

            query = Include(query, includes);

            return query.Where(predicate);

        }

        private IQueryable<TEntity> Include(IQueryable<TEntity> query, string includes = null!)
        {
            if (string.IsNullOrWhiteSpace(includes))
            {
                return query;
            }

            var entitiesToInclude = includes
                .Replace(" ", "")
                .Split(',', StringSplitOptions.RemoveEmptyEntries);

            foreach (string entity in entitiesToInclude)
            {
                query = query.Include(entity);
            }

            return query;

        }

    }

}