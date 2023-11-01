using TransactionAppProject.Models;
namespace TransactionAppProject.Interfaces;

public interface IDataWorkerService
{
    bool PostTransactionDataList(string indexName,  IEnumerable<Transaction> newDatas);
    Task<IEnumerable<Transaction>> AllLinks(string indexName);
    
}