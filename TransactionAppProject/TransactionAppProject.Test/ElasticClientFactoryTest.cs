using TransactionAppProject.Classes;
using Microsoft.Extensions.Configuration;
using Moq;
using TransactionAppProject.Interfaces;

namespace TransactionAppProject.Test;

public class ElasticClientFactoryTest
{
    [Fact]
    public void CreateElasticClientFactory_ReturnNotNullClient()
    {
        // Arrange
        var config = new Mock<IReadConfigurations>();
        config.Setup(x => x.ConfigValues["ElasticsearchURL"]).Returns("http://localhost:2900");
        config.Setup(x => x.ConfigValues["ElasticsearchDebug"]).Returns("True");
        var connection = new ElasticClientFactory(config.Object);
        
        // Act
        var client = connection.GetElasticsearchClient();

        // Assert
        Assert.NotNull(client);
        Assert.Equal(connection.ElasticUri, "http://localhost:2900");
        Assert.Equal(connection.DebugMode, true);
        
    }
}