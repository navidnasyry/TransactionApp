using Nest;
using TransactionAppProject.Models;

namespace TransactionAppProject.Interfaces;

public interface IElasticClientRepository
{
    bool IndexManyData(IEnumerable<Transaction> newData, string indexName);
    Task<IEnumerable<Transaction>> SearchAsyncAllData(string indexName);
    public bool IsIndexExist(string indexName);
    bool CreateIndex(CreateIndexDescriptor indexDescriptor);
    

}