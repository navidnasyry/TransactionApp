using Nest;
namespace TransactionAppProject.Interfaces;

public interface IElasticClientFactory
{
    IElasticClient GetElasticsearchClient();
    string ElasticUri { get; }
    bool DebugMode { get; }
}
