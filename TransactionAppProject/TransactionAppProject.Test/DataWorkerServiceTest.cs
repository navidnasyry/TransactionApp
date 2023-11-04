using Moq;
using TransactionAppProject.Classes;
using Nest;
using TransactionAppProject.ApplicationExceptions;
using TransactionAppProject.Models;
using TransactionAppProject.Interfaces;
using TransactionAppProject.Services;

namespace TransactionAppProject.Test;

public class DataWorkerServiceTest
{
    private readonly string _indexName = "newIndexName";
    private readonly List<Transaction> _dataSample = new()
    {
        new()
        {
            RefrenceNumber = "number1",
            Amount = 29292,
            DestinationAccount = "111199992222",
            SourceAccount = "122200093333",
            TransactionID = "3452224",
            Type = "",
            Date = DateTime.Now
        },
        new()
        {
            RefrenceNumber = "number2",
            Amount = 292000,
            DestinationAccount = "111198892222",
            SourceAccount = "122211093333",
            TransactionID = "3454324",
            Type = "",
            Date = DateTime.Now
        }
    };


    [Fact]
    public void PostTransactionDataList_WithoutException_ReturnTrue()
    {
        // Arrange
        var clientFactory = new Mock<IElasticClientRepository<Transaction>>();
        clientFactory.Setup(x => x.IndexManyData(It.IsAny<IEnumerable<Transaction>>(), It.IsAny<string>())).Returns(true);
        var dataWorker = new DataWorkerService(clientFactory.Object);
        
        
        // Act
         var returnVal = dataWorker.PostTransactionDataList(_indexName, _dataSample);

        // Assert
        Assert.True(returnVal);

    }
    
    [Fact]
    public void PostTransactionDataList_WithoutException_ReturnFalse()
    {
        // Arrange
        var clientFactory = new Mock<IElasticClientRepository<Transaction>>();
        clientFactory.Setup(x => x.IndexManyData(It.IsAny<IEnumerable<Transaction>>(), It.IsAny<string>())).Returns(false);
        var dataWorker = new DataWorkerService(clientFactory.Object);
        
        
        // Act
        var returnVal = dataWorker.PostTransactionDataList(_indexName, _dataSample);

        // Assert
        Assert.False(returnVal);
    }
    [Fact]
    public void PostTransactionDataList_ThrowException_ReturnFalse()
    {
        // Arrange
        var clientFactory = new Mock<IElasticClientRepository<Transaction>>();
        clientFactory.Setup(x => x.IndexManyData(It.IsAny<IEnumerable<Transaction>>(), It.IsAny<string>())).Throws(new Exception());
        var dataWorker = new DataWorkerService(clientFactory.Object);
        
        
        // Act
        var returnVal = dataWorker.PostTransactionDataList(_indexName, _dataSample);

        // Assert
        Assert.False(returnVal);
    }

    [Fact]
    public async Task  AllLinks_ReturnData()
    {
        // Arrange
        var indexName = "index";
        var clientRepo = new Mock<IElasticClientRepository<Transaction>>();
        clientRepo.Setup(c => c
            .SearchAsyncAllData(indexName))
            .Returns(Task.FromResult(_dataSample.AsEnumerable()));
        var dataWorker = new DataWorkerService(clientRepo.Object);
    
        
        // Act
        var result = await dataWorker.AllLinks(indexName);
    
        // Assert
        Assert.Equivalent(result, _dataSample);
        
    
    }
    
    [Fact]
    public async Task AllLinks_ThrowException()
    {
        // Arrange
        var indexName = "index";
        var clientRepo = new Mock<IElasticClientRepository<Transaction>>();
        clientRepo.Setup(c => c
                .SearchAsyncAllData(indexName))
            .Throws(new GetDataFromElasticException());
        var dataWorker = new DataWorkerService(clientRepo.Object);
    
        
        // Act
        var result =  dataWorker.AllLinks;
    
        // Assert
        Assert.ThrowsAsync<GetDataFromElasticException>(async () => await result(indexName));
    
    }
}