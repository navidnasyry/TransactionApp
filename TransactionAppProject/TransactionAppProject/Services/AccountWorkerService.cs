using TransactionAppProject.Interfaces;
using TransactionAppProject.Models;

namespace TransactionAppProject.Services;

public class AccountWorkerService : IAccountWorkerService
{
    private readonly IElasticClientRepository<Account> _clientRepository;


    public AccountWorkerService(IElasticClientRepository<Account> clientFactory)
    {
        _clientRepository = clientFactory;
    }

    public bool PostAccountList(string indexName, IEnumerable<Account> newDatas)
    {
        try
        {

            var result = _clientRepository.IndexManyData(newDatas, indexName);
            if (result)
            {
                return true;
            }
            return false;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
            return false;
        }

        
    }

    public async Task<IEnumerable<Account>> GetAllAccounts(string indexName)
    {
        try
        {
            var response = await _clientRepository.SearchAsyncAllData(indexName);
            return response;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
            throw;
        }
    }

    public async Task<Account> GetAccountDetails(string indexName ,string accountId)
    {
        try
        {
            var response = await _clientRepository.GetOneNodeDetailes(accountId, indexName);
            return response;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
            throw;
        }
    }
    
    
    
}


    


