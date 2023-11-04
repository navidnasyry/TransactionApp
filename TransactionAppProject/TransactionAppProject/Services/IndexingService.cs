using Nest;
using TransactionAppProject.Models;
using TransactionAppProject.Interfaces;
namespace TransactionAppProject.Services;

public class IndexingService : IIndexingService
{

    private readonly IElasticClientRepository<Account> _clientRepository;

    public IndexingService(IElasticClientRepository<Account> clientRepo)
    {
        _clientRepository = clientRepo;
    }

    public bool CreateIndex(string indexName)
    {
        if (!IsIndexNameExist(indexName))
        {
            var returnٰٰٰVal = CreateNewIndex(indexName);
            return returnٰٰٰVal;
        }
        return false;
    }

    public bool IsIndexNameExist(string indexName)
    {
        
        var isExist = _clientRepository.IsIndexExist(indexName);
        return isExist;
    }

    private bool CreateNewIndex(string indexName)
    {
        var indexDescriptor = CreateDescriptor(indexName);
        var indexResponseAck = _clientRepository.CreateIndex(indexDescriptor);
        return indexResponseAck;
    }

    private CreateIndexDescriptor CreateDescriptor(string indexName)
    {
        var indexSetting = CreateIndexSetting();
        var createIndexDescriptor = new CreateIndexDescriptor(indexName)
            .Mappings(ms => ms.Map<Transaction>(m => m.AutoMap()));
        createIndexDescriptor.InitializeUsing(new IndexState { Settings = indexSetting });
        return createIndexDescriptor;
    }

    private IndexSettings CreateIndexSetting()
    {
        var indexSettings = new IndexSettings
        {
            NumberOfReplicas = 1
        };
        return indexSettings;
    }
    
}