

namespace TransactionAppProject.ApplicationExceptions;

[Serializable]
public class ConnectionFailedException: Exception
{
    public ConnectionFailedException(string hostName)
        : base(String.Format("Connection failed to Elasticsearch: {0}", hostName))
    {
        
    }
}
