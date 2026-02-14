using GestionCursos.Core.Interfaces;
using GestionCursos.Infrastructure.Data;
using GestionCursos.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace GestionCursos.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlite(connectionString));

        services.AddScoped<IRepositorioCursos, RepositorioCursos>();

        return services;
    }
}
