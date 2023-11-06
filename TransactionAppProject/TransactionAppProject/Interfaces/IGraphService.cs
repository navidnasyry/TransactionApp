using TransactionAppProject.Models;

namespace TransactionAppProject.Interfaces;

public interface IGraphService
{
    Task<IEnumerable<Graph>> CreateGraph(string nodeIndexName, string linkIndexName);
    Task<IEnumerable<Graph>> ExpandNode(string accountId, string nodeIndexName, string linkIndexName);

}