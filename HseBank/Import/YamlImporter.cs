using System.Collections.Generic;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace HseBank.Import;

public class YamlImporter : BaseImporter<Dictionary<string, object>>
{
    protected override List<Dictionary<string, object>> ParseContent(string content)
    {
        var deserializer = new DeserializerBuilder()
            .WithNamingConvention(CamelCaseNamingConvention.Instance)
            .Build();

        var result = deserializer.Deserialize<List<Dictionary<string, object>>>(content);
        return result ?? new List<Dictionary<string, object>>();
    }
}