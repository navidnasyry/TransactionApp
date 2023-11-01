using Moq;
using Nest;
using TransactionAppProject.Interfaces;
using TransactionAppProject.Models;
using TransactionAppProject.Services;

namespace TransactionAppProject.Test;

public class IndexingServiceTest
{
    
    [Fact]
    public void CreateIndex_IndexNotExist_ReturnTrue()
    {
        // Arrange
        var indexName = "sampleName";
        var clientRepositoryMock = new Mock<IElasticClientRepository>();
        clientRepositoryMock.Setup(x => x.IsIndexExist(indexName)).Returns(false);
        clientRepositoryMock.Setup(x => x.CreateIndex(It.IsAny<CreateIndexDescriptor>())).Returns(true);
        var indexService = new IndexingService(clientRepositoryMock.Object);
    
        // Act
        var returnValue = indexService.CreateIndex(indexName);
    
        // Assert
        Assert.True(returnValue);
    
    }
    [Fact]
    public void CreateIndex_IndexNotExist_ReturnFalse()
    {
        // Arrange
        var indexName = "sampleName";
        var clientRepositoryMock = new Mock<IElasticClientRepository>();
        clientRepositoryMock.Setup(x => x.IsIndexExist(indexName)).Returns(false);
        clientRepositoryMock.Setup(x => x.CreateIndex(It.IsAny<CreateIndexDescriptor>())).Returns(false);
        var indexService = new IndexingService(clientRepositoryMock.Object);
    
        // Act
        var returnValue = indexService.CreateIndex(indexName);
    
        // Assert
        Assert.False(returnValue);
    
    }
    [Fact]
    public void CreateIndex_IndexExist_ReturnFalse()
    {
        // Arrange
        var inValidName = "repeatedName";
        var clientRepositoryMock = new Mock<IElasticClientRepository>();
        clientRepositoryMock.Setup(x => x.IsIndexExist(inValidName)).Returns(true);
        var indexService = new IndexingService(clientRepositoryMock.Object);

        // Act
        var returnValue = indexService.CreateIndex(inValidName);

        // Assert
        Assert.False(returnValue);
    }
    
    [Fact]
    public void IsIndexExist_ReturnFalse()
    {
        // Arrange
        var notExistName = "NotExistName";
        var clientRepositoryMock = new Mock<IElasticClientRepository>();
        clientRepositoryMock.Setup(x => x.IsIndexExist(notExistName)).Returns(false);
        var indexService = new IndexingService(clientRepositoryMock.Object);

        // Act
        var returnValue = indexService.IsIndexNameExist(notExistName);

        // Assert
        Assert.False(returnValue);
    }
    
    [Fact]
    public void IsIndexExist_ReturnTrue()
    {
        // Arrange
        var existName = "ExistName";
        var clientRepositoryMock = new Mock<IElasticClientRepository>();
        clientRepositoryMock.Setup(x => x.IsIndexExist(existName)).Returns(true);
        var indexService = new IndexingService(clientRepositoryMock.Object);

        // Act
        var returnValue = indexService.IsIndexNameExist(existName);

        // Assert
        Assert.True(returnValue);
    }
    
    
}