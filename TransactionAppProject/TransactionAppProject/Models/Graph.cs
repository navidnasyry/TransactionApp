namespace TransactionAppProject.Models;

public class Graph
{
    public required string RefrenceNumber { get; set; }
    public uint Amount { get; set; }
    public Account DestinationAccount { get; set; }
    public Account SourceAccount { get; set; }
    public string TransactionID { get; set; }
    public string Type { get; set; }
    public DateTime Date { get; set; }
    
    
    
}