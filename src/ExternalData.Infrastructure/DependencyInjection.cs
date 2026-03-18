using ExternalData.Domain.Interfaces;
using ExternalData.Infrastructure.Data;
using ExternalData.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ExternalData.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlite(connectionString));

        services.AddScoped<IExchangeRateRepository, ExchangeRateRepository>();

        return services;
    }
}