using Nest;
using TransactionAppProject.Interfaces;
using TransactionAppProject.Models;

namespace TransactionAppProject.Classes;

public class ElasticClientFactory : IElasticClientFactory
{
    private readonly IElasticClient _client = null;
    public ElasticClientFactory(IConfigurationsReader config)
    {
        var _elasticUri = config.ConfigValues["ElasticsearchURL"];
        var _debugMode = bool.Parse(config.ConfigValues["ElasticsearchDebug"]);
        var connectionSettings = CreateConnectionSettingWithUri(_elasticUri, _debugMode);
        _client = new ElasticClient(connectionSettings);
    }
    
    private ConnectionSettings CreateConnectionSettingWithUri(string elasticUri, bool debugMode)
    {
        var uri = new Uri(elasticUri);
        var connectionSetting = new ConnectionSettings(uri);
        if (debugMode)
        {
            connectionSetting.EnableDebugMode();
        }
        return connectionSetting;
    }
    
    public IElasticClient GetElasticsearchClient()
    {
        return _client;
    }
    
    
}