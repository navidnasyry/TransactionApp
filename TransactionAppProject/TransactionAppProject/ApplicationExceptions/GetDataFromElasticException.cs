namespace TransactionAppProject.ApplicationExceptions;

[Serializable]
public class GetDataFromElasticException: Exception
{
    public GetDataFromElasticException()
        : base("Get data from Elasticsearch failed !!!")

    {

    }
}