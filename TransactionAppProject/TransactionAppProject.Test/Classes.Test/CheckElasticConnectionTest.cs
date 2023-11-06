using TransactionAppProject.Classes;
using Moq;
using Nest;

namespace TransactionAppProject.Test;

public class CheckElasticConnectionTest
{
    [Fact]
    public void AllChecker_ReturnTrue()
    {
        // Arrange
        var mockElasticClient = new Mock<IElasticClient>();
        mockElasticClient.Setup(
            x => x.Ping(
                It.IsAny<Func<PingDescriptor, IPingRequest>>()
                ).ToString()).Returns("ping response is 200");
        
        var checkElasticConnection = new CheckElasticConnection(mockElasticClient.Object);

        // Act
        var result = checkElasticConnection.CheckAllCheckers();

        // Assert
        Assert.True(result);
        
    }
}