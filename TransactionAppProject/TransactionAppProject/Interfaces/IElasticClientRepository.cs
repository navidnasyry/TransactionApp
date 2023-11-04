using Nest;
using TransactionAppProject.Models;

namespace TransactionAppProject.Interfaces;

public interface IElasticClientRepository<T> 
{
    bool IndexManyData(IEnumerable<T> newData, string indexName);
    Task<IEnumerable<T>> SearchAsyncAllData(string indexName);
    public bool IsIndexExist(string indexName);
    bool CreateIndex(CreateIndexDescriptor indexDescriptor);
    Task<Account> GetOneNodeDetailes(string AccountID, string indexName);

}