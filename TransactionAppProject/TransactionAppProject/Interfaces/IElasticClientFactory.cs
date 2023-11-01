using Nest; 
using TransactionAppProject.Models;
namespace TransactionAppProject.Interfaces;

public interface IElasticClientFactory
{
    IElasticClient GetElasticsearchClient();

}
