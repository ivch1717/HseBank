using HseBank.Facades;
using HseBank.Import;

namespace HseBank.Commands.ImportCommand;

public class ImportCategoriesFromYaml : ICommand<string>
{
    private readonly ICategoryFacade _facade;
    private readonly IImportResolver _importResolver;

    public ImportCategoriesFromYaml(ICategoryFacade facade, IImportResolver importResolver)
    {
        _facade = facade;
        _importResolver = importResolver;
    }

    public void Execute(string filepath)
    {
        var records = _importResolver.GetImporter<Dictionary<string, object>>("yaml").Import(filepath);

        foreach (var record in records)
        {
            try
            {
                string? nameKey = record.Keys.FirstOrDefault(k => k.Equals("Name", StringComparison.OrdinalIgnoreCase));
                string? typeKey = record.Keys.FirstOrDefault(k => k.Equals("Type", StringComparison.OrdinalIgnoreCase));

                if (nameKey == null || typeKey == null)
                    continue;

                string name = record[nameKey]?.ToString()?.Trim() ?? "";
                string typeName = record[typeKey]?.ToString()?.Trim() ?? "";

                if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(typeName))
                    continue;

                _facade.AddCategory(name, typeName);
            }
            catch (ArgumentException)
            {
               continue;
            }
            
        }
    }
}