using TransactionAppProject.Interfaces;

namespace TransactionAppProject.Classes;

public class ReadConfigurations : IReadConfigurations
{
    public IConfigurationRoot ConfigValues
    {
        get;
    }

    public ReadConfigurations(string filePath)
    {
        IConfigurationBuilder builder = new ConfigurationBuilder().AddXmlFile(
            filePath, false, true);
        ConfigValues = builder.Build();
    }
}