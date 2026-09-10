using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

public static class DependancyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
    {
        var cosmosConnectionString = config["CosmosDb:ConnectionString"]
            ?? throw new InvalidOperationException("CosmosDb:ConnectionString is not configured.");
        var cosmosDatabaseName = config["CosmosDb:DatabaseName"]
            ?? throw new InvalidOperationException("CosmosDb:DatabaseName is not configured.");

        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseCosmos(cosmosConnectionString, cosmosDatabaseName)
        );
        services.AddScoped<IApplicationDbContext>(sp => sp.GetRequiredService<ApplicationDbContext>());
        services.AddScoped<ISkillRepository, SkillRepository>();
        
        return services;
    }
}