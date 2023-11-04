namespace TransactionAppProject.Models;

public class Transaction
{
    public string RefrenceNumber { get; set; }
    
    public uint Amount { get; set; }
    public string DestinationAccount { get; set; }
    public string SourceAccount { get; set; }
    
    public string TransactionID { get; set; }
    
    public string Type { get; set; }
    
    public DateTime Date { get; set; }
}