namespace HseBank.Export;

public class ExportResolver : IExportResolver
{
    private readonly Dictionary<string, Func<IExportVisitor>> _visitorFactories = new()
    {
        { "json", () => new ExportToJsonVisitor() },
        { "csv",  () => new ExportToCsvVisitor() },
        { "yaml", () => new ExportToYamlVisitor() }
    };
    
    public IExportVisitor GetVisitor(string visitorName)
    {
        if (!_visitorFactories.ContainsKey(visitorName))
        {
            throw new ArgumentException($"неправильное расширение: '{visitorName}'");
        }
        
        return _visitorFactories[visitorName]();
    }
}