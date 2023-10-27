using Nest;
using TransactionAppProject.Interfaces;

namespace TransactionAppProject.Classes;

public class ElasticClientFactory : IElasticClientFactory
{
    private readonly IElasticClient _client = null;
    public string ElasticUri
    {
        get;
    }
    public bool DebugMode
    {
        get;
    }
    public ElasticClientFactory(IReadConfigurations config)
    {
        ElasticUri = config.ConfigValues["ElasticsearchURL"];
        DebugMode = bool.Parse(config.ConfigValues["ElasticsearchDebug"]);

        var connectionSettings = CreateConnectionSettingWithUri(ElasticUri, DebugMode);
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