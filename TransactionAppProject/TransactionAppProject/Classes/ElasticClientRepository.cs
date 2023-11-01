using TransactionAppProject.Models;
using TransactionAppProject.Interfaces;
using TransactionAppProject.ApplicationExceptions;

using Nest;


namespace TransactionAppProject.Classes;

public class ElasticClientRepository : IElasticClientRepository
{
    private readonly IElasticClient _client;

    public ElasticClientRepository(IElasticClientFactory clientFactory)
    {
        _client = clientFactory.GetElasticsearchClient();
    }
    
    public bool IndexManyData(IEnumerable<Transaction> newData, string indexName)
    {
        var response = _client.IndexMany(newData, indexName);
        return response.IsValid;
    }
    
    public async Task<IEnumerable<Transaction>> SearchAsyncAllData(string indexName)
    {
        var result = await _client.SearchAsync<Transaction>(
            t => t
                .Index(indexName)
                .MatchAll()
        );
        if (!result.IsValid)
        {
            throw new GetDataFromElasticException();
        }
        return result.Documents.ToList();
    }
    
    public bool IsIndexExist(string indexName)
    {
        var isExist = _client.Indices.Exists(indexName);
        return isExist.Exists;
    }

    public bool CreateIndex(CreateIndexDescriptor indexDescriptor)
    {
        var createIndexResponse = _client.Indices.Create(indexDescriptor);
        return createIndexResponse.Acknowledged;
    }
    
}