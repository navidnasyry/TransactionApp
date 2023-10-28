using TransactionAppProject.Classes;
using System.IO;
namespace TransactionAppProject.Test;

public class ReadConfigurationsTest
{
    [Fact]
    public void ReadConfigurationsValue_IsCorrect()
    {
        // Arrange
        var testConfigFile = "appTest.config";
        var config = new ReadConfigurations(testConfigFile);
        var expectedValues = new Dictionary<string, string>()
        {
            { "ElasticsearchURL", "http://localhost:9200" },
            {"ElasticsearchDebug", "True"}
        };
        // Act
        var configValues = config.ConfigValues;
        
        // Assert
        Assert.Equal(expectedValues["ElasticsearchURL"], configValues["ElasticsearchURL"]);
        Assert.Equal(expectedValues["ElasticsearchDebug"], configValues["ElasticsearchDebug"]);

    }

    [Fact]
    public void ReadConfigurationValue_PathNotFound()
    {
        // Arrange
        var invalidFilePath = "ThisFileNotExist.config";
        
        // Act and Assert
        Assert.Throws<FileNotFoundException>(() => new ReadConfigurations(invalidFilePath));
    }
    
}