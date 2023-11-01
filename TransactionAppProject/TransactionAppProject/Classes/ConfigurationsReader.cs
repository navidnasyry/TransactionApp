using TransactionAppProject.Interfaces;

namespace TransactionAppProject.Classes;

public class ConfigurationsReader : IConfigurationsReader
{
    public IConfigurationRoot ConfigValues
    {
        get;
    }

    public ConfigurationsReader(IConfigurationBuilder filePathObj, string filePath)
    {
        var builder = filePathObj.AddXmlFile(
            filePath, false, true);
        ConfigValues = builder.Build();
    }
}