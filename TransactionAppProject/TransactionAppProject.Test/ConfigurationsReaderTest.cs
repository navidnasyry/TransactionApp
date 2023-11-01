using TransactionAppProject.Classes;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace TransactionAppProject.Test;

public class ConfigurationsReaderTest
{
    [Fact]
    public void ReadConfigurationsValue_IsCorrect()
    {
        // Arrange
        var testConfigFile = "appTest.config";
        var builderObj = new ConfigurationBuilder();
        var config = new ConfigurationsReader(builderObj, testConfigFile);
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
        var builderObj = new ConfigurationBuilder();

        // Act
        var act = () => new ConfigurationsReader(builderObj, invalidFilePath);
        
        // Assert
        Assert.Throws<FileNotFoundException>(act);
    }
    
}