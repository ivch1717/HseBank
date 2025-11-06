using HseBank.Facades;
using HseBank.Import;

namespace HseBank.Commands.ImportCommand;

public class ImportOperationsFromYaml : ICommand<string>
{
    private readonly IOperationFacade _facade;
    private readonly IImportResolver _importResolver;

    public ImportOperationsFromYaml(IOperationFacade facade, IImportResolver importResolver)
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
                string? accKey = record.Keys.FirstOrDefault(k => k.Equals("BankAccountId", StringComparison.OrdinalIgnoreCase));
                string? catKey = record.Keys.FirstOrDefault(k => k.Equals("CategoryId", StringComparison.OrdinalIgnoreCase));
                string? amtKey = record.Keys.FirstOrDefault(k => k.Equals("Amount", StringComparison.OrdinalIgnoreCase));
                string? descKey = record.Keys.FirstOrDefault(k => k.Equals("Description", StringComparison.OrdinalIgnoreCase));

                if (accKey == null || catKey == null || amtKey == null)
                    continue;

                if (!int.TryParse(record[accKey]?.ToString(), out int accountId))
                    continue;
                if (!int.TryParse(record[catKey]?.ToString(), out int categoryId))
                    continue;
                if (!int.TryParse(record[amtKey]?.ToString(), out int amount))
                    continue;

                string description = descKey != null ? record[descKey]?.ToString() ?? "" : "";

                _facade.AddOperation(accountId, amount, categoryId, description);
            }
            catch (ArgumentException)
            {
               continue;
            }
        }
    }
}