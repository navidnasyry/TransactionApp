using TransactionAppProject.Interfaces;
using TransactionAppProject.Services;
using TransactionAppProject.Models;
using TransactionAppProject.Classes;
using TransactionAppProject.ApplicationExceptions;
namespace TransactionAppProject.Configs;

public static class MyConfigServiceCollectionExtensions
{
    public static IServiceCollection AddDependencyGroup(
        this IServiceCollection services)
    {
        // Read Environment Configs
        var configFilePath = "app.config";
        var builderObj = new ConfigurationBuilder();
        var configObj = new ConfigurationsReader(builderObj, configFilePath);

        // Connecting to Elasticsearch
        var elasticObj = new ElasticClientFactory(configObj);
        var elasticClient = elasticObj.GetElasticsearchClient();

        var connectionChecker = new CheckElasticConnection(elasticClient);
        if (!connectionChecker.CheckAllCheckers())
        {
            throw new ConnectionFailedException(elasticObj.ToString());
        }

        
        // Create Elastic Repository
        var elasticRepository = new ElasticClientRepository<Account>(elasticObj);
        var elasticRepositoryTransaction = new ElasticClientRepository<Transaction>(elasticObj);
        services.AddSingleton<IElasticClientRepository<Account>>(x=>elasticRepository);
        services.AddSingleton<IElasticClientRepository<Transaction>>(x=>elasticRepositoryTransaction);

        
        services.AddScoped<IIndexingService, IndexingService>();
        services.AddScoped<IDataWorkerService, DataWorkerService>();
        services.AddScoped<IAccountWorkerService, AccountWorkerService>();

        return services;
    }
}