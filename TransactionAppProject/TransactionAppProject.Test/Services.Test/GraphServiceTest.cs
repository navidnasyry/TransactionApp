using Moq;
using TransactionAppProject.ApplicationExceptions;
using TransactionAppProject.Interfaces;
using TransactionAppProject.Models;
using TransactionAppProject.Services;

namespace TransactionAppProject.Test.Services.Test;

public class GraphServiceTest
{
    private readonly Mock<IElasticClientRepository<Transaction>> _clientRepositoryMock = new Mock<IElasticClientRepository<Transaction>>();
    private readonly List<Graph> _graphDataList;

    private readonly List<Transaction> _dataTransaction = new()
    {
        new()
        {
            RefrenceNumber = "222222",
            Amount = 29292,
            DestinationAccount = "1",
            SourceAccount = "2",
            TransactionID = "3452224",
            Type = "1",
            Date = DateTime.Now
        },
        new()
        {
            RefrenceNumber = "111111",
            Amount = 292000,
            DestinationAccount = "2",
            SourceAccount = "1",
            TransactionID = "3454324",
            Type = "2",
            Date = DateTime.Now
        }
    };

    public GraphServiceTest()
    {
        var destinationAccount = new Account()
        {
            AccountID = "1",
            CardID = "123339933",
            Shaba = "IR456789876",
            AccountType = "chom",
            BranchTelephone = "000999999",
            BranchAdress = "addr",
            BranchName = "choom",
            OwnerName = "name",
            OwnerFamilyName = "skdjf",
            OwnerID = "993993"
        };
        var sourceAccount = new Account()
        {
            AccountID = "2",
            CardID = "123339933",
            Shaba = "IR456789876",
            AccountType = "chom",
            BranchTelephone = "000999999",
            BranchAdress = "addr",
            BranchName = "choom",
            OwnerName = "name",
            OwnerFamilyName = "skdjf",
            OwnerID = "993993"
        };

        _graphDataList = new()
        {
            new()
            {
                RefrenceNumber = "222222",
                Amount = 29292,
                DestinationAccount = destinationAccount,
                SourceAccount = sourceAccount,
                TransactionID = "3452224",
                Type = "1",
                Date = DateTime.Now
            },
            new()
            {
                RefrenceNumber = "111111",
                Amount = 292000,
                DestinationAccount = sourceAccount,
                SourceAccount = destinationAccount,
                TransactionID = "3454324",
                Type = "2",
                Date = DateTime.Now
            }

        };
    }

    public void CreateGraph_ThrowException()
    {
        // Arrange
        var nodeIndex = "nodeIndex";
        var linkIndexName = "linkIndex";
        _clientRepositoryMock.Setup(x => x.SearchAsyncAllData(linkIndexName)).Returns(Task.FromResult(_dataTransaction.AsEnumerable()));
        var graphService = new GraphService(_clientRepositoryMock.Object);

        // Act
        var returnVal = graphService.CreateGraph;
        
        // Assert
        Assert.ThrowsAsync<GetDataFromElasticException>(async () => await returnVal(nodeIndex, linkIndexName));
        
    }
    [Fact]
    public async Task CreateGraph_ReturnData()
    {
        // Arrange
        var nodeIndex = "nodeIndex";
        var linkIndexName = "linkIndex";
        _clientRepositoryMock.Setup(x => x.SearchAsyncAllData(linkIndexName)).Returns(Task.FromResult(_dataTransaction.AsEnumerable()));
        var graphService = new GraphService(_clientRepositoryMock.Object);

        // Act
        var returnVal = await graphService.CreateGraph(nodeIndex, linkIndexName);
        
        // Assert
        Assert.Equal(_graphDataList.Count, returnVal.Count());
    }
    
    
   
}