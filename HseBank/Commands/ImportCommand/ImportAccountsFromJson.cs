using HseBank.Facades;
using HseBank.Import;
namespace HseBank.Commands.ImportCommand;

public class ImportAccountsFromJson : ICommand<string>
{
    private readonly IBankAccountFacade _facade;
    private readonly IImportResolver _importResolver;

    public ImportAccountsFromJson(IBankAccountFacade facade, IImportResolver importResolver)
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
                if (!record.ContainsKey("Name") || !record.ContainsKey("Balance"))
                    continue;

                string name = record["Name"]?.ToString()?.Trim() ?? "";
                if (string.IsNullOrWhiteSpace(name))
                    continue;

                if (!int.TryParse(record["Balance"]?.ToString(), out int balance))
                    continue;

                _facade.AddAccount(name, balance);
            }
            catch (ArgumentException)
            {
                continue;
            }
        }
    }
}