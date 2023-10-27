namespace TransactionAppProject.Models;

public class Transaction
{

    public string RefrenceNumber { get; set; }
    
    public uint TransactionValue { get; set; }
    
    public string DestinationAccount { get; set; }
    
    public string SourceAccount { get; set; }
    
    
}