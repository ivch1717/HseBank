using HseBank.Facades;
using HseBank.Import;

namespace HseBank.Commands.ImportCommand;

public class ImportCategoriesFromJson : ICommand<string>
{
    private readonly ICategoryFacade _facade;
    private readonly IImportResolver _importResolver;

    public ImportCategoriesFromJson(ICategoryFacade facade, IImportResolver importResolver)
    {
        _facade = facade;
        _importResolver = importResolver;
    }

    public void Execute(string filepath)
    {
        var records = _importResolver.GetImporter<Dictionary<string, object>>("json").Import(filepath);

        foreach (var record in records)
        {
            try
            {
                if (!record.ContainsKey("Name") || !record.ContainsKey("Type"))
                    continue;

                string name = record["Name"]?.ToString()?.Trim() ?? "";
                string typeName = record["Type"]?.ToString()?.Trim() ?? "";

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