namespace HseBank.Import;

public class ImportResolver : IImportResolver
{
    private readonly Dictionary<string, Func<object>> _importers = new();

    public ImportResolver()
    {
        _importers["csv"] = () => new CsvImporter();
        _importers["json"] = () => new JsonImporter();
        _importers["yaml"] = () => new YamlImporter();
    }

    public IImporter<T> GetImporter<T>(string name)
    {
        if (!_importers.ContainsKey(name))
        {
            throw new ArgumentException("непраильный формат файла");
        }
        return (IImporter<T>)_importers[name].Invoke();
    }
}