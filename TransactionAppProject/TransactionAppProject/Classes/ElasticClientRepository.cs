using TransactionAppProject.Models;
using TransactionAppProject.Interfaces;
using TransactionAppProject.ApplicationExceptions;

using Nest;


namespace TransactionAppProject.Classes;

public class ElasticClientRepository<T> : IElasticClientRepository<T> where T : class
{
    private readonly IElasticClient _client;

    public ElasticClientRepository(IElasticClientFactory clientFactory)
    {
        _client = clientFactory.GetElasticsearchClient();
    }
    
    public bool IndexManyData(IEnumerable<T> newData, string indexName)
    {
        var response = _client.IndexMany(newData, indexName);
        return response.IsValid;
    }
    
    public async Task<IEnumerable<T>> SearchAsyncAllData(string indexName)
    {
        var result = await _client.SearchAsync<T>(
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


    public async Task<Account> GetOneNodeDetailes(string AccountID, string indexName)
    {
        var response = await _client.SearchAsync<Account>(s => s
            .Index(indexName)
            .Query(q => q
                .Match(m => m
                    .Field(f => f.AccountID)
                    .Query(AccountID)
                )
            )
        );
        if (!response.IsValid)
        {
            throw new GetDataFromElasticException();
        }

        return response.Documents.ToList()[0];
    }
    
    
}