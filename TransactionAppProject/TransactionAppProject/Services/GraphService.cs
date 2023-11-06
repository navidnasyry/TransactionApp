using TransactionAppProject.Models;
using TransactionAppProject.Interfaces;

namespace TransactionAppProject.Services;

public class GraphService : IGraphService
{
    private readonly IElasticClientRepository<Transaction> _clientRepository;

    public GraphService(IElasticClientRepository<Transaction> clientRepository)
    {
        _clientRepository = clientRepository;
    }


    public async Task<IEnumerable<Graph>> CreateGraph(string nodeIndexName, string linkIndexName)
    {
        try
        {
            var response = new List<Graph>();
            var allTransactions = await _clientRepository.SearchAsyncAllData(linkIndexName);
            foreach (var transaction in allTransactions)
            {
                var newMember = CreateNewGraphMember(transaction, nodeIndexName);
                response.Add(newMember);
            }

            return response;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
            throw;
        }
    }

    private Graph CreateNewGraphMember(Transaction transaction, string nodeIndexName)
    {
        var destinationAccoutn = _clientRepository.GetOneNodeDetailes(transaction.DestinationAccount, nodeIndexName);
        var sourceAccoutn = _clientRepository.GetOneNodeDetailes(transaction.SourceAccount, nodeIndexName);
        var newMember = new Graph()
        {
            RefrenceNumber = transaction.RefrenceNumber,
            Amount = transaction.Amount,
            TransactionID = transaction.TransactionID,
            Type = transaction.Type,
            Date = transaction.Date,
            DestinationAccount = destinationAccoutn.Result,
            SourceAccount = sourceAccoutn.Result
        };
        return newMember;
    }
    
    public async Task<IEnumerable<Graph>> ExpandNode(string accountId, string nodeIndexName, string linkIndexName)
    {

        try
        {
            var response = new List<Graph>();
            var allTransactions = await _clientRepository.ExpandOneNode(nodeIndexName, accountId);
            foreach (var transaction in allTransactions)
            {
                var destinationAccoutn = _clientRepository.GetOneNodeDetailes(transaction.DestinationAccount, nodeIndexName);
                var sourceAccoutn = _clientRepository.GetOneNodeDetailes(transaction.SourceAccount, nodeIndexName);
                var newMember = new Graph()
                {
                    RefrenceNumber = transaction.RefrenceNumber,
                    Amount = transaction.Amount,
                    TransactionID = transaction.TransactionID,
                    Type = transaction.Type,
                    Date = transaction.Date,
                    DestinationAccount = destinationAccoutn.Result,
                    SourceAccount = sourceAccoutn.Result
                };
                response.Add(newMember);
            }

            return response;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
            throw;
        }
    }


    
    
}