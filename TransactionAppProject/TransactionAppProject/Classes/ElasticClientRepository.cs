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


    public async Task<Account> GetOneNodeDetailes(string AccountId, string indexName)
    {
        try
        {
            var response = await _client.SearchAsync<Account>(s => s
                .Index(indexName)
                .Query(q => q
                    .Match(m => m
                        .Field(f => f.AccountID)
                        .Query(AccountId)
                    )
                )
            );
            Console.WriteLine(response);
            if (!response.IsValid)
            {
                throw new GetDataFromElasticException();
            }

            if (response.Documents.Count == 0)
            {
                return null;
            }
            return response.Documents.ToList()[0];
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<IEnumerable<Transaction>> ExpandOneNode(string indexName, string accountId)
    {
        try
        {
            var response = await _client.SearchAsync<Transaction>(i =>
                i.Index(indexName)
                    .Query(q=>q
                        .Bool(b=>b
                            .Should(s=>s
                                .Term(m=>m
                                    .Field(f=>f.SourceAccount)
                                    .Value(accountId)
                                ),
                                e=>e
                                    .Term(g=>g
                                        .Field(i=>i.DestinationAccount)
                                        .Value(accountId)
                                    )
                            )
                        )
                    )
            );
            return response.Documents.ToList();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new GetDataFromElasticException();
        }
    }
    
}