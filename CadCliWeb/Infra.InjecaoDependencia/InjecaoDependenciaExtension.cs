using Dominio.Interfaces.Dados;
using Dominio.Interfaces.Servicos;
using Infra.Dados;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Servicos;

namespace Infra.InjecaoDependencia
{
    public static class InjecaoDependenciaExtension
    {
        public static IServiceCollection AddInjecaoDependencia(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddDbContext<Contexto>(options =>
                {
                    var connectionString = configuration.GetConnectionString("DefaultConnection");

                    options.UseSqlServer(connectionString);
                    options.EnableSensitiveDataLogging(true);

                }, ServiceLifetime.Transient);

            services.AddTransient<IUnitOfWork, UnitOfWork<Contexto>>();
            services.AddTransient<IUnitOfWork<Contexto>, UnitOfWork<Contexto>>();

            services.AddTransient<IClientesService, ClientesServico>();

            return services;

        }

    }

}