using System.Collections.Generic;
using System.Text.Json;

namespace HseBank.Import;

public class JsonImporter : BaseImporter<Dictionary<string, object>>
{
    protected override List<Dictionary<string, object>> ParseContent(string content)
    {
        var elements = JsonSerializer.Deserialize<List<Dictionary<string, JsonElement>>>(content)!;
        var result = new List<Dictionary<string, object>>();

        foreach (var obj in elements)
        {
            var dict = new Dictionary<string, object>();
            foreach (var kvp in obj)
            {
                dict[kvp.Key] = kvp.Value.ValueKind switch
                {
                    JsonValueKind.String => kvp.Value.GetString()!,
                    JsonValueKind.Number => kvp.Value.GetDouble(),
                    JsonValueKind.True => true,
                    JsonValueKind.False => false,
                    _ => kvp.Value.ToString()!
                };
            }
            result.Add(dict);
        }

        return result;
    }
}