using TransactionAppProject.Interfaces;

namespace TransactionAppProject.Classes;

public class ConfigurationsReader : IConfigurationsReader
{
    public IConfigurationRoot ConfigValues
    {
        get;
    }

    public ConfigurationsReader(string filePath)
    {
        IConfigurationBuilder builder = new ConfigurationBuilder().AddXmlFile(
            filePath, false, true);
        ConfigValues = builder.Build();
    }
}