using Nest;
using TransactionAppProject.Models;
using TransactionAppProject.Interfaces;
namespace TransactionAppProject.Services;

public class IndexingService : IIndexingService
{

    private readonly IElasticClient _client;

    public IndexingService(IElasticClientFactory client)
    {
        _client = client.GetElasticsearchClient();
    }

    public bool CreateIndex(string indexName)
    {
        if (!IsIndexExist(indexName))
        {
            var returnٰٰٰVal = CreateNewIndex(indexName);
            return returnٰٰٰVal;
        }
        return false;
    }

    public bool IsIndexExist(string indexName)
    {
        
        Console.WriteLine(indexName);
        var isExist = _client.Indices.Exists(indexName);
        return isExist.Exists;
    }

    private bool CreateNewIndex(string indexName)
    {
        var indexDescriptor = CreateDescriptor(indexName);
        var createIndexResponse = _client.Indices.Create(indexDescriptor);
        return createIndexResponse.Acknowledged;
    }

    private CreateIndexDescriptor CreateDescriptor(string indexName)
    {
        var indexSetting = CreateIndexSetting();
        var createIndexDescriptor = new CreateIndexDescriptor(indexName)
            .Mappings(ms => ms.Map<Transaction>(m => m.AutoMap()));
        createIndexDescriptor.InitializeUsing(new IndexState { Settings = indexSetting });
        return createIndexDescriptor;
    }

    private IndexSettings CreateIndexSetting()
    {
        var indexSettings = new IndexSettings
        {
            NumberOfReplicas = 1
        };
        return indexSettings;
    }
    
}