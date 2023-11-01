using TransactionAppProject.Interfaces;
using TransactionAppProject.Services;

namespace TransactionAppProject.Configs;

public static class MyConfigServiceCollectionExtensions
{
    public static IServiceCollection AddDependencyGroup(
        this IServiceCollection services)
    {
        services.AddScoped<IIndexingService, IndexingService>();
        services.AddScoped<IDataWorkerService, DataWorkerService>();

        return services;
    }
}