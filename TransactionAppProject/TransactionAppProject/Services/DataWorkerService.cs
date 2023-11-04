using Nest;
using TransactionAppProject.ApplicationExceptions;
using TransactionAppProject.Interfaces;
using TransactionAppProject.Models;

namespace TransactionAppProject.Services;

public class DataWorkerService : IDataWorkerService
{
    private readonly IElasticClientRepository<Transaction> _clientRepository;

    public DataWorkerService(IElasticClientRepository<Transaction> clientFactory)
    {
        _clientRepository = clientFactory;
    }

    public bool PostTransactionDataList(string indexName, IEnumerable<Transaction> newDatas)
    {
        try
        {
            var responseIsValid = _clientRepository.IndexManyData(newDatas, indexName);
            if (responseIsValid)
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

    public async Task<IEnumerable<Transaction>> AllLinks(string indexName)
    {
        try
        {
            var responseDataDocument = await _clientRepository.SearchAsyncAllData(indexName);
            return responseDataDocument; 
        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
            throw;
        }
    }
    
}