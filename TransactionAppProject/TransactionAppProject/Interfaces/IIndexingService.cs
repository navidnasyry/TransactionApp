namespace TransactionAppProject.Interfaces;

public interface IIndexingService
{
    public bool CreateIndex(string indexName);
    public bool IsIndexNameExist(string indexName);
    
}