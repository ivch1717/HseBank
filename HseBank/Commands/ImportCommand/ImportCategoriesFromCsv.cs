using HseBank.Facades;
using HseBank.Import;

namespace HseBank.Commands.ImportCommand;

public class ImportCategoriesFromCsv : ICommand<string>
{
    private readonly ICategoryFacade _facade;
    private readonly IImportResolver _importResolver;

    public ImportCategoriesFromCsv(ICategoryFacade facade, IImportResolver importResolver)
    {
        _facade = facade;
        _importResolver = importResolver;
    }

    public void Execute(string filepath)
    {
        var rows = _importResolver.GetImporter<string[]>("csv").Import(filepath);

        foreach (var row in rows)
        {
            try
            {
                if (row.Length == 0)
                    continue;

                if (row.Any(col => col.Trim().Equals("Name", StringComparison.OrdinalIgnoreCase)))
                    continue;

                string name, typeName;

                if (row.Length >= 3)
                {
                    name = row[1].Trim();
                    typeName = row[2].Trim();
                }
                else if (row.Length == 2)
                {
                    name = row[0].Trim();
                    typeName = row[1].Trim();
                }
                else
                {
                    continue;
                }

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