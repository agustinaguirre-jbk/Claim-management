using DeltaApi.Domain.Repositories;
using DeltaApi.Infrastructure.Data;
using DeltaApi.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace DeltaApi.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, string connectionString)
    {
        // Register connection factory
        services.AddScoped<IConnectionFactory>(provider => new ConnectionFactory(connectionString));
        
        // Register repositories
        services.AddScoped<IClaimRepository, ClaimRepository>();
        services.AddScoped<IDoctorRepository, DoctorRepository>();
        services.AddScoped<IClaimTypeRepository, ClaimTypeRepository>();
        services.AddScoped<IStateOfLossRepository, StateOfLossRepository>();
        
        return services;
    }
}
