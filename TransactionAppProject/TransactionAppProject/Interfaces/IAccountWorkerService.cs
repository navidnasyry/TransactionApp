using TransactionAppProject.Models;

namespace TransactionAppProject.Interfaces;

public interface IAccountWorkerService
{
    bool PostAccountList(string indexName, IEnumerable<Account> newDatas);
    Task<IEnumerable<Account>> GetAllAccounts(string indexName);
    Task<Account> GetAccountDetails(string indexName, string accountID);




}