using HseBank.Facades;
using HseBank.Import;

namespace HseBank.Commands.ImportCommand;

public class ImportOperationsFromJson : ICommand<string>
{
    private readonly IOperationFacade _facade;
    private readonly IImportResolver _importResolver;

    public ImportOperationsFromJson(IOperationFacade facade, IImportResolver importResolver)
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
                if (!record.ContainsKey("BankAccountId") || 
                    !record.ContainsKey("CategoryId") || 
                    !record.ContainsKey("Amount"))
                    continue;

                if (!int.TryParse(record["BankAccountId"]?.ToString(), out int accountId))
                    continue;
                if (!int.TryParse(record["CategoryId"]?.ToString(), out int categoryId))
                    continue;
                if (!int.TryParse(record["Amount"]?.ToString(), out int amount))
                    continue;

                string description = record.ContainsKey("Description") ? record["Description"]?.ToString() ?? "" : "";
                _facade.AddOperation(accountId, amount, categoryId, description);
            }
            catch (ArgumentException)
            {
               continue;
            }
        }
    }
}