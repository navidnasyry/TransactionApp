using Nest;
using TransactionAppProject.Interfaces;

namespace TransactionAppProject.Classes;

public class CheckElasticConnection : ICheckElasticConnection
{
    private readonly IElasticClient _client = null;
    
    public CheckElasticConnection(IElasticClient client)
    {
        _client = client;
    }

    public bool CheckAllCheckers()
    {
        return PingChecker();
    }
    private bool PingChecker()
    {
        if (_client.Ping().ToString().Contains("200"))
        {
            return true;
        }
        return false;
        
    }
    
}