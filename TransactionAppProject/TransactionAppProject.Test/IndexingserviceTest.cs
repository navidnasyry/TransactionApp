using Moq;
using Nest;
using TransactionAppProject.Interfaces;
using TransactionAppProject.Services;

namespace TransactionAppProject.Test;

public class IndexingserviceTest
{
    private readonly Mock<IElasticClientFactory> _clientFactoryMock;
    private readonly Mock<IElasticClient> _elasticClientMock;
    private readonly IIndexingService _indexingService;
    
    public IndexingserviceTest()
    {
        _clientFactoryMock = new Mock<IElasticClientFactory>();
        _elasticClientMock = new Mock<IElasticClient>();
        _clientFactoryMock.Setup(x => x.GetElasticsearchClient()).Returns(_elasticClientMock.Object);
        _indexingService = new IndexingService(_clientFactoryMock.Object);
    }
    
    [Fact]
    public void CreateIndex_ReturnTrue()
    {
        // Arrange
        var indexName = "IndexNotNameExist";
        _elasticClientMock.Setup(x => x.Indices.Exists(indexName, null).Exists).Returns(false);
        _elasticClientMock.Setup(x => x.Indices.Create(It.IsAny<CreateIndexDescriptor>()).Acknowledged).Returns(true);
        
        // Act
        var res = _indexingService.CreateIndex(indexName);
        
        // Assert
        Assert.True(res);
    }
    [Fact]
    public void CreateIndex_ReturnFalse()
    {
        // Arrange
        var indexName = "IndexNameExist";
        _elasticClientMock.Setup(x => x.Indices.Exists(indexName, null).Exists).Returns(true);
        
        // Act
        var res = _indexingService.CreateIndex(indexName);
        
        // Assert
        Assert.True(res);
    }
    
}