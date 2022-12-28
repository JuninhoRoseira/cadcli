using Dominio.Entidades;
using Microsoft.EntityFrameworkCore;

namespace Infra.Dados
{
    public class Contexto : DbContext
    {
        public Contexto(DbContextOptions<Contexto> options) : base(options) { }

        public virtual DbSet<Cliente> Clientes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            //optionsBuilder.UseDataFactorySqlServerQuerySqlGenerator();
            //optionsBuilder.EnableSensitiveDataLogging();
            //optionsBuilder.UseLoggerFactory(_loggerFactory);

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfigurationsFromAssembly(typeof(Contexto).Assembly);

            foreach (var property in builder.Model.GetEntityTypes()
                .SelectMany(e => e.GetProperties()
                    .Where(p => p.ClrType == typeof(DateTime) || p.ClrType == typeof(DateTime?))))
            {
                property.SetColumnType("datetime");
            }

        }

    }
}