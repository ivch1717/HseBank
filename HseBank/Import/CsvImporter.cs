using System.Collections.Generic;
using System.Linq;

namespace HseBank.Import;

public class CsvImporter : BaseImporter<string[]>
{
    protected override List<string[]> ParseContent(string content)
    {
        var lines = content.Split('\n', StringSplitOptions.RemoveEmptyEntries);
        return lines.Select(line => line.Trim().Split(',')).ToList();
    }
}